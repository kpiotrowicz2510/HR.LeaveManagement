using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _repository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository repository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _repository = repository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(x=>x.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .MustAsync(LeaveAllocationMustExistAsync).WithMessage("{PropertyName} does not exist.");;
        
        RuleFor(x=>x.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(x=>x.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}.");

        RuleFor(x => x.LeaveTypeId)
            .GreaterThan(0)
            .WithMessage("Leave Type Id is required")
            .MustAsync(LeaveTypeMustExistAsync)
            .WithMessage("Leave Type does not exist");
    }

    private async Task<bool> LeaveAllocationMustExistAsync(int id, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
        return leaveAllocation != null;
    }
    
    private async Task<bool> LeaveTypeMustExistAsync(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _repository.GetByIdAsync(id);
        return leaveType != null;
    }
}
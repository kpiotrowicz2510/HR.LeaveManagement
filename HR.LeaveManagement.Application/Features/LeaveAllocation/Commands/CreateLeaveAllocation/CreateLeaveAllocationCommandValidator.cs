using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.LeaveTypeId)
            .GreaterThan(0)
            .WithMessage("Leave Type Id is required")
            .MustAsync(LeaveTypeMustExistAsync)
            .WithMessage("Leave Type does not exist");
    }

    private async Task<bool> LeaveTypeMustExistAsync(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _repository.GetByIdAsync(id);
        return leaveType != null;
    }
}
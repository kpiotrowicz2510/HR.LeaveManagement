using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator :
    AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;
        RuleFor(p=>p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("Name cannot be more than 70 characters.");
        RuleFor(p=>p.DefaultDays)
            .GreaterThan(1).WithMessage("Default days must be greater than 1.")
            .LessThan(100).WithMessage("Default days must be less than 100.");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique).WithMessage("Leave type must be unique");
    }

    private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return await _repository.IsLeaveTypeUniqueAsync(command.Name);
    }
}
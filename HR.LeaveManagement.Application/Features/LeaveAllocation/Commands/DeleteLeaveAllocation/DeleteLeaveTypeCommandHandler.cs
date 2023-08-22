using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _repository;

    public DeleteLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        //Validate
        var leaveAllocation = await _repository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        await _repository.DeleteAsync(leaveAllocation);
        
        return Unit.Value;
    }
}
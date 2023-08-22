using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;

public record GetLeaveAllocationDetailQuery(int Id) : IRequest<LeaveAllocationDetailsDto>;
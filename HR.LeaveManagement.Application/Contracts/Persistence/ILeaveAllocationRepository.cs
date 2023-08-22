using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsForUserAsync(string userId);
    Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period);
    Task AddAllocationsAsync(List<LeaveAllocation> leaveAllocations);
    Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId);
}
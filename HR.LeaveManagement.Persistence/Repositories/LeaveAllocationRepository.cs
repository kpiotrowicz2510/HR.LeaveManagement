using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
    {
        return await _context
            .LeaveAllocations
            .Include(x=>x.LeaveType)
            .FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
    {
        return await _context
            .LeaveAllocations
            .Include(x=>x.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsForUserAsync(string userId)
    {
        return await _context
            .LeaveAllocations
            .Where(x=>x.EmployeeId == userId)
            .Include(x=>x.LeaveType)
            .ToListAsync();
    }

    public async Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations
            .AnyAsync(x=>x.EmployeeId == userId 
            && x.LeaveTypeId == leaveTypeId && x.Period == period);
    }

    public async Task AddAllocationsAsync(List<LeaveAllocation> leaveAllocations)
    {
        await _context.LeaveAllocations.AddRangeAsync(leaveAllocations);
    }

    public async Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId)
    {
        return await _context
            .LeaveAllocations
            .FirstOrDefaultAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId);
    }
}
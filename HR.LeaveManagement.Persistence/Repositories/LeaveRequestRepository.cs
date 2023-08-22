using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
    {
        return await _context
            .LeaveRequests
            .Include(x=>x.LeaveType)
            .FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
    {
        return await _context
            .LeaveRequests
            .Include(x=>x.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsForUserAsync(string userId)
    {
        return await _context
            .LeaveRequests
            .Where(x=>x.RequestingEmployeeId == userId)
            .Include(x=>x.LeaveType)
            .ToListAsync();
    }
}
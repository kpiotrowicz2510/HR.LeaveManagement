using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Persistence.Providers;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class HrDatabaseContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public HrDatabaseContext(IDateTimeProvider dateTimeProvider, DbContextOptions<HrDatabaseContext> options) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>()
                     .Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.DateModified = _dateTimeProvider.GetUtcNow();
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = _dateTimeProvider.GetUtcNow();
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}
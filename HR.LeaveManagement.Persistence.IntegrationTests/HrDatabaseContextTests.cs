using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Providers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDatabaseContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;

    public HrDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _dateTimeProvider
            .Setup(x => x.GetUtcNow())
            .Returns(
                new DateTime(2020, 1, 1, 0, 0, 0)
            );
        
        _hrDatabaseContext = new HrDatabaseContext(_dateTimeProvider.Object, dbOptions);
    }
    
    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        //Arrange
        var createDateTime = _dateTimeProvider.Object.GetUtcNow();
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };
        
        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateCreated.ShouldBe(createDateTime);
        leaveType.DateModified.ShouldBe(createDateTime);
    }
    
    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        var createDateTime = _dateTimeProvider.Object.GetUtcNow();
        
        var leaveTypeToModify = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };
        
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveTypeToModify);
        await _hrDatabaseContext.SaveChangesAsync();
        
        var leaveType = await _hrDatabaseContext.LeaveTypes.FirstAsync(x=>x.Id == leaveTypeToModify.Id);

        leaveType.DefaultDays = 20;
        
        // Act
        var modifiedDate = new DateTime(2021, 1, 1, 0, 0, 0);
        _dateTimeProvider.Setup(x => x.GetUtcNow()).Returns(modifiedDate);
        
        _hrDatabaseContext.LeaveTypes.Update(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateCreated.ShouldBe(createDateTime);
        leaveType.DateModified.ShouldBe(modifiedDate);
        leaveType.DefaultDays.ShouldBe(20);
    }
}
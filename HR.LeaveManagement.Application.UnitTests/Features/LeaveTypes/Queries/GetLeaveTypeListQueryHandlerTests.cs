using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeListQueryHandlerTests()
    {
        _mockRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        
        var mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new LeaveTypeProfile());
            });
        
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepository.Object);
        
        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);
        
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}
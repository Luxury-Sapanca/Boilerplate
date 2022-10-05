namespace Boilerplate.Api.Tests.Controllers;

public class DummyControllerTests
{
    private readonly Mock<IDummyService> _mockDummyService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly DummyController _sut;

    public DummyControllerTests()
    {
        _mockDummyService = new Mock<IDummyService>();
        _mockMapper = new Mock<IMapper>();
        _sut = new DummyController(_mockDummyService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Dummy_GetAsync_ShouldReturnAllDummies()
    {
        //Arrange
        var mockDummyDto = new List<DummyDto>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 2, Name = "Test2" }
        };
        var mockGetDummiesResponse = new List<GetDummyResponse>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 2, Name = "Test2" }
        };
        _mockDummyService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockDummyDto);
        _mockMapper.Setup(m => m.Map<List<GetDummyResponse>>(mockDummyDto)).Returns(mockGetDummiesResponse);

        //Act
        var result = await _sut.GetAsync();
        var resultObject = (ObjectResult)result.Result;

        //Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(expected: mockGetDummiesResponse, actual: resultObject!.Value);
        _mockDummyService.VerifyAll();
    }

    [Fact]
    public async Task Dummy_GetAsync_WithGivenId_ShouldReturnAllDummy()
    {
        //Arrange
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        var mockGetDummyResponse = new GetDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyService.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummyDto);
        _mockMapper.Setup(m => m.Map<GetDummyResponse>(mockDummyDto)).Returns(mockGetDummyResponse);

        //Act
        var result = await _sut.GetAsync(1);
        var resultObject = (ObjectResult)result.Result;

        //Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(expected: mockGetDummyResponse, actual: resultObject!.Value);
        _mockDummyService.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PostAsync_WithGivenDummy_ShouldAddDummy()
    {
        //Arrange
        var mockCreateDummyRequest = new CreateDummyRequest
        {
            Name = "Test"
        };
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        var mockCreateDummyResponse = new CreateDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyService.Setup(s => s.PostAsync(It.IsAny<DummyDto>())).ReturnsAsync(mockDummyDto);
        _mockMapper.Setup(m => m.Map<DummyDto>(mockCreateDummyRequest)).Returns(mockDummyDto);
        _mockMapper.Setup(m => m.Map<CreateDummyResponse>(mockDummyDto)).Returns(mockCreateDummyResponse);


        //Act
        var result = await _sut.PostAsync(mockCreateDummyRequest);
        var resultObject = (ObjectResult)result.Result;

        //Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(expected: mockCreateDummyResponse, actual: resultObject!.Value);
        _mockDummyService.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PutAsync_WithGivenDummy_ShouldUpdateDummy()
    {
        //Arrange
        var mockUpdateDummyRequest = new UpdateDummyRequest
        {
            Id = 1,
            Name = "Test"
        };
        var mockUpdateDummyResponse = new UpdateDummyResponse
        {
            Id = 1,
            Name = "Test2"
        };
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test2"
        };
        _mockDummyService.Setup(s => s.PutAsync(It.IsAny<DummyDto>())).ReturnsAsync(mockDummyDto);
        _mockMapper.Setup(m => m.Map<DummyDto>(mockUpdateDummyRequest)).Returns(mockDummyDto);
        _mockMapper.Setup(m => m.Map<UpdateDummyResponse>(mockDummyDto)).Returns(mockUpdateDummyResponse);

        //Act
        var result = await _sut.PutAsync(mockUpdateDummyRequest);
        var resultObject = (ObjectResult)result.Result;

        //Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(expected: mockUpdateDummyResponse, actual: resultObject!.Value);
        _mockDummyService.VerifyAll();
    }

    [Fact]
    public async Task Dummy_DeleteAsync_WithGivenDummy_ShouldDeleteDummy()
    {
        //Arrange
        _mockDummyService.Setup(s => s.DeleteAsync(It.IsAny<int>()));

        //Act
        await _sut.DeleteAsync(1);

        //Assert
        _mockDummyService.VerifyAll();
    }
}
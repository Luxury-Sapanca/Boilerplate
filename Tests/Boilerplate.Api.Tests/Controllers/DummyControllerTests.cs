using Boilerplate.Api.Controllers;
using Boilerplate.Domain.Requests.Dummy;
using Boilerplate.Domain.Responses.Dummy;
using Boilerplate.Service.Interfaces;

namespace Boilerplate.Api.Tests.Controllers;

public class DummyControllerTests
{
    private readonly Mock<IDummyService> _mockDummyService;
    private readonly DummyController _sut;

    public DummyControllerTests()
    {
        _mockDummyService = new Mock<IDummyService>();
        _sut = new DummyController(_mockDummyService.Object);
    }

    [Fact]
    public async Task Dummy_GetAsync_ShouldReturnAllDummies()
    {
        //Arrange
        var mockGetDummiesResponse = new List<GetDummyResponse>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 2, Name = "Test2" }
        };
        _mockDummyService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockGetDummiesResponse);

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
        var mockDummyResponse = new GetDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyService.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummyResponse);

        //Act
        var result = await _sut.GetAsync(1);
        var resultObject = (ObjectResult)result.Result;

        //Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(expected: mockDummyResponse, actual: resultObject!.Value);
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
        var mockCreateDummyResponse = new CreateDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyService.Setup(s => s.PostAsync(It.IsAny<CreateDummyRequest>())).ReturnsAsync(mockCreateDummyResponse);

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
            Id = 2,
            Name = "Test2"
        };
        _mockDummyService.Setup(s => s.PutAsync(It.IsAny<UpdateDummyRequest>())).ReturnsAsync(mockUpdateDummyResponse);

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
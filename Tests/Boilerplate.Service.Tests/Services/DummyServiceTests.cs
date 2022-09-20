using Boilerplate.Data.Repository;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Exceptions;
using Boilerplate.Domain.Requests.Dummy;
using Boilerplate.Domain.Responses.Dummy;
using Boilerplate.Service.Services;

namespace Boilerplate.Service.Tests.Services;

public class DummyServiceTests
{
    private readonly Mock<IGenericRepository<Dummy>> _mockSurchargeRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly DummyService _dummyService;

    public DummyServiceTests()
    {
        _mockSurchargeRepository = new Mock<IGenericRepository<Dummy>>();
        _mockMapper = new Mock<IMapper>();
        _dummyService = new DummyService(_mockSurchargeRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Dummy_GetAsync_ShouldReturnAllDummiesResponse()
    {
        //Arrange
        var mockDummies = new List<Dummy>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 1, Name = "Test2" }
        };
        var mockDummiesResponse = new List<GetDummyResponse>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 1, Name = "Test2" }
        };
        _mockSurchargeRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(mockDummies);
        _mockMapper.Setup(m => m.Map<List<GetDummyResponse>>(It.IsAny<List<Dummy>>())).Returns(mockDummiesResponse);

        //Act
        var result = await _dummyService.GetAllAsync();

        //Assert
        Assert.Equal(expected: result, actual: mockDummiesResponse);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_GetAsync_WithGivenId_ShouldReturnDummyResponse()
    {
        //Arrange
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        var mockDummyResponse = new GetDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummy);
        _mockMapper.Setup(m => m.Map<GetDummyResponse>(It.IsAny<Dummy>())).Returns(mockDummyResponse);

        //Act
        var result = await _dummyService.GetAsync(1);

        //Assert
        Assert.Equal(expected: result, actual: mockDummyResponse);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PostAsync_WithGivenCreateDummyRequest_ShouldReturnCreateDummyResponse()
    {
        //Arrange
        var mockCreateDummyRequest = new CreateDummyRequest
        {
            Name = "Test"
        };
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        var mockCreateDummyResponse = new CreateDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.AddAsync(It.IsAny<Dummy>())).ReturnsAsync(mockDummy);
        _mockMapper.Setup(m => m.Map<CreateDummyResponse>(It.IsAny<Dummy>())).Returns(mockCreateDummyResponse);

        //Act
        var result = await _dummyService.PostAsync(mockCreateDummyRequest);

        //Assert
        Assert.Equal(expected: result, actual: mockCreateDummyResponse);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PostAsync_WithGivenCreateDummyRequest_ShouldThrowExistingRecordException_IfRecordExists()
    {
        //Arrange
        var mockCreateDummyRequest = new CreateDummyRequest
        {
            Name = "Test"
        };
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(p => p.Name == mockCreateDummyRequest.Name)).ReturnsAsync(mockDummy);

        //Act
        Task Result() => _dummyService.PostAsync(mockCreateDummyRequest);

        //Assert
        var exception = await Assert.ThrowsAsync<DummyException>(Result);
        Assert.Equal("There is a dummy. Name: 'Test'", exception.Message);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PutAsync_WithGivenUpdateDummyRequest_ShouldReturnCreateDummyResponse()
    {
        //Arrange
        var mockUpdateDummyRequest = new UpdateDummyRequest
        {
            Id = 1,
            Name = "Test"
        };
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        var mockUpdateDummyResponse = new UpdateDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummy);
        _mockSurchargeRepository.Setup(s => s.UpdateAsync(It.IsAny<Dummy>())).ReturnsAsync(mockDummy);
        _mockMapper.Setup(m => m.Map<UpdateDummyResponse>(It.IsAny<Dummy>())).Returns(mockUpdateDummyResponse);

        //Act
        var result = await _dummyService.PutAsync(mockUpdateDummyRequest);

        //Assert
        Assert.Equal(expected: result, actual: mockUpdateDummyResponse);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PutAsync_WithGivenUpdateDummyRequest_ShouldThrowRecordNotFoundException_IfRecordDoesNotExist()
    {
        //Arrange
        var mockUpdateDummyRequest = new UpdateDummyRequest
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(mockUpdateDummyRequest.Id)).ReturnsAsync((Dummy)null);

        //Act
        Task Result() => _dummyService.PutAsync(mockUpdateDummyRequest);

        //Assert
        var exception = await Assert.ThrowsAsync<DummyException>(Result);
        Assert.Equal("Dummy is not found while updating. DummyId: '1'", exception.Message);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_DeleteAsync_WithGivenId_ShouldBeVerified()
    {
        //Arrange
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummy);
        _mockSurchargeRepository.Setup(s => s.DeleteAsync(It.IsAny<Dummy>()));

        //Act
        await _dummyService.DeleteAsync(1);

        //Assert
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_DeleteAsync_WithGivenId_ShouldThrowRecordNotFoundException_IfRecordDoesNotExist()
    {
        //Arrange
        _mockSurchargeRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((Dummy)null);

        //Act
        Task Result() => _dummyService.DeleteAsync(5);

        //Act-Assert
        var exception = await Assert.ThrowsAsync<DummyException>(Result);
        Assert.Equal("Dummy is not found while deleting. DummyId: '5'", exception.Message);
        _mockSurchargeRepository.VerifyAll();
    }
}
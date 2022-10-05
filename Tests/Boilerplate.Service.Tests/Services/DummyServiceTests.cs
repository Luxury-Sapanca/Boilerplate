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
        var mockDummiesResponse = new List<DummyDto>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 1, Name = "Test2" }
        };
        _mockSurchargeRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(mockDummies);
        _mockMapper.Setup(m => m.Map<List<DummyDto>>(It.IsAny<List<Dummy>>())).Returns(mockDummiesResponse);

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
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummy);
        _mockMapper.Setup(m => m.Map<DummyDto>(It.IsAny<Dummy>())).Returns(mockDummyDto);

        //Act
        var result = await _dummyService.GetAsync(1);

        //Assert
        Assert.Equal(expected: result, actual: mockDummyDto);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PostAsync_WithGivenCreateDummyRequest_ShouldReturnCreateDummyResponse()
    {
        //Arrange
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.AddAsync(It.IsAny<Dummy>())).ReturnsAsync(mockDummy);
        _mockMapper.Setup(m => m.Map<DummyDto>(It.IsAny<Dummy>())).Returns(mockDummyDto);

        //Act
        var result = await _dummyService.PostAsync(mockDummyDto);

        //Assert
        Assert.Equal(expected: result, actual: mockDummyDto);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PostAsync_WithGivenCreateDummyRequest_ShouldThrowExistingRecordException_IfRecordExists()
    {
        //Arrange
        var mockDummyDto = new DummyDto
        {
            Name = "Test"
        };
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(p => p.Name == mockDummyDto.Name)).ReturnsAsync(mockDummy);

        //Act
        Task Result() => _dummyService.PostAsync(mockDummyDto);

        //Assert
        var exception = await Assert.ThrowsAsync<DummyException>(Result);
        Assert.Equal("There is a dummy. Name: 'Test'", exception.Message);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PutAsync_WithGivenUpdateDummyRequest_ShouldReturnCreateDummyResponse()
    {
        //Arrange
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummy);
        _mockSurchargeRepository.Setup(s => s.UpdateAsync(It.IsAny<Dummy>())).ReturnsAsync(mockDummy);
        _mockMapper.Setup(m => m.Map<DummyDto>(It.IsAny<Dummy>())).Returns(mockDummyDto);

        //Act
        var result = await _dummyService.PutAsync(mockDummyDto);

        //Assert
        Assert.Equal(expected: result, actual: mockDummyDto);
        _mockSurchargeRepository.VerifyAll();
    }

    [Fact]
    public async Task Dummy_PutAsync_WithGivenUpdateDummyRequest_ShouldThrowRecordNotFoundException_IfRecordDoesNotExist()
    {
        //Arrange
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        _mockSurchargeRepository.Setup(s => s.GetAsync(mockDummyDto.Id)).ReturnsAsync((Dummy)null);

        //Act
        Task Result() => _dummyService.PutAsync(mockDummyDto);

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
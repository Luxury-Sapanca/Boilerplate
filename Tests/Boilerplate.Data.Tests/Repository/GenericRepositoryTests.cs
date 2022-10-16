namespace Boilerplate.Data.Tests.Repository;

public class GenericRepositoryTests : IDisposable
{
    private readonly DataContext _dataContext;

    public GenericRepositoryTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseInMemoryDatabase("test");

        _dataContext = new DataContext(optionsBuilder.Options);
    }
    
    public void Dispose()
    {
        _dataContext.Database.EnsureDeleted();
    }

    [Fact]
    public async Task GenericRepository_GetAllAsync_ShouldReturnAllDummies()
    {
        //Arrange
        var data = new List<Dummy>
        {
            new() { Id = 1, Name = "AAA" },
            new() { Id = 2, Name = "BBB" },
            new() { Id = 3, Name = "CCC" },
        };

        _dataContext.Dummies.AddRange(data);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummies = await repository.GetAllAsync();

        //Assert
        Assert.Equal(data.Count, dummies.Count);
    }

    [Fact]
    public async Task GenericRepository_GetAsync_ShouldReturnDummy()
    {
        //Arrange
        var data = new Dummy { Id = 1, Name = "AAA" };
        _dataContext.Dummies.Add(data);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummy = await repository.GetAsync(data.Id);

        //Assert
        Assert.Equal(data.Id, dummy.Id);
        Assert.Equal(data.Name, dummy.Name);
    }
}
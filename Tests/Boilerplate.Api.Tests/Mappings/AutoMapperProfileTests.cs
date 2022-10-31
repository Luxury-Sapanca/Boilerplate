namespace Boilerplate.Api.Tests.Mappings;

public class AutoMapperProfileTests
{
    [Fact]
    public Task ApiAutoMapper_ValidateMappings_ShouldBeValid()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());

        });
        IMapper mapper = new Mapper(mapperConfig);
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
        return Task.CompletedTask;
    }
}
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Boilerplate.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions<AuthenticationConfiguration>(AuthenticationConfiguration.AuthenticationScheme)
            .Bind(Configuration.GetSection(nameof(AuthenticationConfiguration)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(Configuration.GetSection("ConnectionStrings:DummyDb").Value)
        );

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IDummyService, DummyService>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext, IConfigurationProvider autoMapperConfiguration)
    {
        if (env.IsDevelopment())
        {
            autoMapperConfiguration.AssertConfigurationIsValid();

            app.UseDeveloperExceptionPage();
        }

        dataContext.Database.Migrate();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
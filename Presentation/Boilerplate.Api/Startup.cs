using Boilerplate.Api.Security.Authentication;
using Microsoft.OpenApi.Models;

namespace Boilerplate.Api;

[ExcludeFromCodeCoverage]
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
        services.AddOptions<AuthenticationConfiguration>(AuthenticationConfiguration.AuthenticationScheme).Bind(Configuration.GetSection(nameof(AuthenticationConfiguration))).ValidateDataAnnotations().ValidateOnStart();
        services.AddDbContextPool<DataContext>(options => options.UseSqlite(Configuration.GetSection("ConnectionStrings:DummyDb").Value));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IDummyService, DummyService>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Just enter token without Bearer.",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });

        });

        #region Authentcation

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = AuthenticationConfiguration.AuthenticationScheme;
            x.DefaultChallengeScheme = AuthenticationConfiguration.AuthenticationScheme;
        }).AddScheme<AuthenticationConfiguration, AuthenticationHandler>(AuthenticationConfiguration.AuthenticationScheme, _ => { });
        services.AddScoped<IAuthenticationHandler, AuthenticationHandler>();

        #endregion

        #region Authorization

        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(AuthorizationRequirement), policy => policy.Requirements.Add(new AuthorizationRequirement()));
        });
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthorizationHandler, AuthorizationHandler>();

        #endregion
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
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "swagger";
            options.SwaggerEndpoint("v1/swagger.json", "Boilerplate API v1");
        });
    }
}
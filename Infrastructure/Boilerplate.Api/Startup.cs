using Boilerplate.Api.Middlewares;
using Boilerplate.Data;
using Boilerplate.Data.Repository;
using Boilerplate.Service.Interfaces;
using Boilerplate.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
    {
        if (env.IsDevelopment())
        {
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
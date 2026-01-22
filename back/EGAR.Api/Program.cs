using EGAR.Api.Configure;
using EGAR.Application;
using EGAR.Infrastructure;

namespace EGAR.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        ConfigureMiddlewares(app, app.Environment, builder.Configuration);
        app.Run();
    }

    static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        ConfigureApiService.Configure(services, configuration);
        services.AddApplication();
        services.AddInfrastructure(configuration);
    }

    static void ConfigureMiddlewares(WebApplication app, IWebHostEnvironment env, IConfiguration configuration)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseAuthorization();
        app.MapControllers();
    }
}

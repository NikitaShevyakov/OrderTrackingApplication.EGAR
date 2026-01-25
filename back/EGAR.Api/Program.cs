using EGAR.Api.Configure;
using EGAR.Application;
using EGAR.Infrastructure;

namespace EGAR.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureConfiguration(builder.Configuration, builder.Environment);
        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        ConfigureMiddlewares(app, app.Environment, builder.Configuration);
        app.Run();
    }

    private static void ConfigureConfiguration(
        ConfigurationManager configuration,
        IWebHostEnvironment environment)
    {
        var solutionRoot = FindSolutionRoot();
        var commonConfigPath = Path.Combine(solutionRoot, "appsettings.common.json");
        if (File.Exists(commonConfigPath))
        {
            configuration.AddJsonFile(commonConfigPath, optional: false, reloadOnChange: true);
        }
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

        app.UseCors("AllowAll");

        //app.UseAuthorization();
        app.MapControllers();
    }

    static string FindSolutionRoot()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (dir != null && !dir.GetFiles("*.sln").Any())
        {
            dir = dir.Parent;
        }

        return dir?.FullName ?? Directory.GetCurrentDirectory();
    }
}

using EGAR.Infrastructure.Data.MsSQL;
using EGAR.Infrastructure.Data.MsSQL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        var assemblyDir = AppContext.BaseDirectory;
        var commonSettingsPath = Path.Combine(assemblyDir, "appsettings.common.json");

        config.AddJsonFile(commonSettingsPath, optional: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = context.Configuration.GetConnectionString(DbOptions.MsSqlConnection);
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("EGAR.Migrations"));
        });
    })
    .Build();

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

Console.WriteLine("Applying migrations...");
db.Database.Migrate();
Console.WriteLine("Done.");

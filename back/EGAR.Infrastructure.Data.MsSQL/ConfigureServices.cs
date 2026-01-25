using EGAR.Application.Interfaces.Repositories;
using EGAR.Infrastructure.Data.MsSQL.Context;
using EGAR.Infrastructure.Data.MsSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGAR.Infrastructure.Data.MsSQL;

public static class ConfigureServices
{
    public static IServiceCollection RegisterDataMsSQLServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(DbOptions.MsSqlConnection));
            options.EnableSensitiveDataLogging(true);
        });

        return services;
    }
}
using EGAR.Application.Interfaces;
using EGAR.Infrastructure.Data.MsSQL;
using EGAR.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGAR.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataMsSQLServices(configuration);
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}

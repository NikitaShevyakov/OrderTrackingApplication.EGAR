using EGAR.Infrastructure.Data.MsSQL;
using EGAR.Infrastructure.RabbitMQ;
using EGAR.Infrastructure.RabbitMQ.Publishers;
using EGAR.SharedKernel.Publishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGAR.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataMsSQLServices(configuration);
        services.RegisterRabbitMQServices(configuration);
        services.AddTransient<IPublisher, RabbitMqPublisher>();

        return services;
    }
}

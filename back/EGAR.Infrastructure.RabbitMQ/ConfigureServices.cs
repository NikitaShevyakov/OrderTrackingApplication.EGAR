using EGAR.Infrastructure.RabbitMQ.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGAR.Infrastructure.RabbitMQ;

public static class ConfigureServices
{
    public static IServiceCollection RegisterRabbitMQServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderStatusUpdatedConsumer>();            

            var rabbitMqConfig = configuration.GetSection("RabbitMq");

            if (rabbitMqConfig.Exists())
            {
                var rabbitMqOptions = new RabbitMqOptions();
                rabbitMqConfig.Bind(rabbitMqOptions);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(
                        rabbitMqOptions.Host,
                        rabbitMqOptions.Port,
                        rabbitMqOptions.VirtualHost,
                        h =>
                        {
                            h.Username(rabbitMqOptions.UserName);
                            h.Password(rabbitMqOptions.Password);
                        });

                    cfg.ConfigureEndpoints(context);
                });
            }
            else
            {
                x.UsingInMemory();
            }
        });

        //services.AddMassTransitHostedService();

        return services;
    }
}
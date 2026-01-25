using EGAR.Application.Behaviors;
using EGAR.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EGAR.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ResultPipelineBehavior<,>));
        });

        services.AddSingleton<InMemoryOrderStatusEventStream>();
        services.AddSingleton<IOrderStatusEventStream>(
            sp => sp.GetRequiredService<InMemoryOrderStatusEventStream>());
        services.AddSingleton<IOrderStatusEventDispatcher>(
            sp => sp.GetRequiredService<InMemoryOrderStatusEventStream>());

        return services;
    }
}

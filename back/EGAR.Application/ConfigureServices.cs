using EGAR.Application.Behaviors;
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

        return services;
    }
}

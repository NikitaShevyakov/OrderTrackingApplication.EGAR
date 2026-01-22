using EGAR.Application.Interfaces;
using EGAR.Infrastructure.Data.MsSQL.Repositories;
using EGAR.Infrastructure.Data.MsSQL.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGAR.Infrastructure.Data.MsSQL;

public static class ConfigureServices
{
    public static IServiceCollection RegisterDataMsSQLServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(MsSqlRepository<>));
        services.AddScoped<IOrderRepository, OrderRepository>();

        //services.AddScoped<ITeamRepository, TeamRepository>();
        //services.AddScoped<ISourceRepository, SourceRepository>();
        //services.AddScoped<IGeoRepository, GeoRepository>();
        //services.AddScoped<IFinStateLineRepository, FinStateLineRepository>();
        //services.AddScoped<IAvailableOptionsRepository, AvailableOptionsRepository>();
        //services.AddScoped<IProjectProvider, ProjectProvider>();
        //services.AddScoped<IProjectRepository, ProjectRepository>();
        //services.AddTransient<IIdentityService, IdentityService>();
        //services.AddTransient<IUserListProvider, UserListProvider>();
        //services.AddTransient<ITeamListProvider, TeamListProvider>();
        //services.AddTransient<IWhitelistIpProvider, WhitelistIpProvider>();
        //services.AddTransient<IExpensesRecalculationStatusProvider, ExpensesRecalculationStatusRepository>();
        //services.AddTransient<IExpensesRecalculationStatusRepository, ExpensesRecalculationStatusRepository>();
        //services.AddDbContext<ApplicationDbContext>(options =>
        //{
        //    options.UseSqlServer(configuration.GetConnectionString(DbOptions.DefaultConnectionName));
        //    options.EnableSensitiveDataLogging(true);
        //});
        //services.AddHttpContextAccessor();
        //services.AddScoped<ICancellationTokenProvider, HttpContextCancellationTokenProvider>();

        return services;
    }
}
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Services.ProjectServices;
using TaskManager.Srv.Services.UtilityServices;
using TaskManager.Srv.Services.WiLinkService;

namespace TaskManager.Srv.Extensions;

/// <summary>
/// Regisztrálja a szolgáltatásokat
/// </summary>
public static class ServiceRegistrationExtension
{
    public static IServiceCollection RegisterDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultConns = configuration.GetConnectionString("default");
        services.AddPooledDbContextFactory<ManagerContext>(options => options
            .UseSqlServer(defaultConns)
            .UseSnakeCaseNamingConvention());

        return services;
    }

    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IClaimsTransformation, ClaimsTransformation>();
        services.AddScoped<IProjectAdminService, ProjectAdminService>();
        services.AddScoped<IProjectDisplayService, ProjectDisplayService>();
        services.AddScoped<IProjectViewService, ProjectViewService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWiLinkTemplateService, WiLinkTemplateService>();
        return services;
    }
}

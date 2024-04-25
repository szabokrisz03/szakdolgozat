using FluentValidation;

using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

using TaskManager_02.Data.Context;
using TaskManager_02.Data.ViewModels;
using TaskManager_02.Extensions.Validators;
using TaskManager_02.Services.ProjectServices;
using TaskManager_02.Services.UserServices;

namespace TaskManager_02.Extensions;

public static class ServiceRegistrationExtension
{
    /// <summary>
    /// 
    ///     Létrehozza az adatbázis kapcsolatot, majd hozzáadja azt az alkalmazáshoz.
    /// 
    /// </summary>
    public static IServiceCollection RegisterDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("default");
        services.AddPooledDbContextFactory<TaskManagerContext>(options => options
        .UseSqlServer(connectionString));

        return services;
    }

    /// <summary>
    /// 
    ///     Létrehozza, majd hozzá adja a szükséges függőségeket az alkalamazáshoz.
    /// 
    /// </summary>
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddTransient<IClaimsTransformation, ClaimsTransformation>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectViewService, ProjectViewService>();

        //Validátorok
        services.AddScoped<IValidator<ProjectViewModel>, ProjectValidator>();

        return services;
    }
}

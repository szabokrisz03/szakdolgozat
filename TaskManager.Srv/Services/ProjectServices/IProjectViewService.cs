namespace TaskManager.Srv.Services.ProjectServices;

/// <summary>
/// MEssage-ek és formok megjelenítésére
/// </summary>
public interface IProjectViewService
{
    Task CreateProjectAsync(string userName = "");
}

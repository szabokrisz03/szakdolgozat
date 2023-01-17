using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.ProjectServices;

public interface IProjectDisplayService
{
    Task<bool> ProjectIdExistsAsync(long rowid);
    Task<List<ProjectViewModel>> ListProjectsAsync(string searchTerm, int take, int skip = 0);
    Task<List<ProjectViewModel>> ListUserProjectsAsync(string userName, string searchTerm, int take, int skip = 0);
    Task<List<ProjectViewModel>> ListUserProjectsAsync(string userName, DateTime visitedUntil, int take, int skip = 0);
    Task<ProjectViewModel?> GetProjectAsync(Guid technicalName);
    Task<string?> GetProjectNameAsync(Guid technicalName);
    Task<ProjectViewModel?> GetProjectAsync(long rowId);
    Task<bool> ProjectTechnicalNameExistsAsync(Guid technicalName);
    Task<bool> ProjectNameExistsAsync(string name);
}

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.ProjectServices;

public interface IProjectAdminService
{
    Task<ProjectViewModel> CreateProjectAsync(ProjectViewModel projectView);
    Task<ProjectViewModel> CreateProjectAsync(ProjectViewModel projectView, string userName);
    Task<bool> AssignProjectUserAsync(long projectid, string userName);
}

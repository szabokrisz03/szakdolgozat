namespace TaskManager.Srv.Services.TaskServices;

public interface ITaskDisplayService
{
    Task<bool> TaskNameExistsAsync(long projectId, string name);
}

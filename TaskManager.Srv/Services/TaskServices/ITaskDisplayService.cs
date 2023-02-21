namespace TaskManager.Srv.Services.TaskServices;

public interface ITaskDisplayService
{
    Task<bool> TaskNameExistsAsync(string name);
}

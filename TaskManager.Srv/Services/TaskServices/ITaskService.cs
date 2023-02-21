using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.TaskServices;

public interface ITaskService
{
    Task<TaskViewModel> CreateTask(TaskViewModel taskView);
}
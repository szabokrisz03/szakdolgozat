using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.TaskServices;

public interface ITaskService
{
    Task<TaskViewModel> CreateTask(TaskViewModel taskView);
    Task<int> CountTasks(long projectId);
    Task<List<TaskViewModel>> ListTasks(long projectId, int take = 10, int skip = 0);
    Task UpdateTask(TaskViewModel taskState);
}
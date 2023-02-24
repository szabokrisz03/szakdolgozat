using Microsoft.AspNetCore.Components;

using MudBlazor;
using TaskManager.Srv.Components.Dialogs;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.TaskServices;

public class TaskViewService : ITaskViewService
{
    private readonly IDialogService dialogService;
    private readonly ITaskService taskService;

    [CascadingParameter] private MudDialogInstance Dialog { get; set; } = null!;

    public TaskViewService(IDialogService dialogService, ITaskService taskService)
    {
        this.dialogService = dialogService;
        this.taskService = taskService;
    }

    public async Task CreateTaskDialog(string technicalName)
    {
        var dialog = await dialogService.ShowAsync<CreateTaskDialog>("Új Feladat");
        var result = await dialog.Result;

        if (result != null)
        {
            TaskViewModel taskViewModel = (TaskViewModel)result.Data;
            await taskService.CreateTask(taskViewModel);
        }


        //lekerni a projct id-t, beleirni a resultba, itt kell meghivni a create taskot -> kivenni a dialogbol
    }
}

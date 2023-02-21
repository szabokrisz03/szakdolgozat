using MudBlazor;
using TaskManager.Srv.Components.Dialogs;

namespace TaskManager.Srv.Services.TaskServices;

public class TaskViewService : ITaskViewService
{
    private readonly IDialogService dialogService;

    public TaskViewService(IDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    public async Task CreateTaskDialog()
    {
        var dialog = await dialogService.ShowAsync<CreateTaskDialog>("Új Feladat");
        await dialog.Result;
    }
}

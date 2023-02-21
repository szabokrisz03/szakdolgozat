using Microsoft.AspNetCore.Components;
using MudBlazor;

using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.ProjectServices;
using TaskManager.Srv.Services.TaskServices;

namespace TaskManager.Srv.Components.Dialogs;

public partial class CreateTaskDialog
{
    private TaskViewModel taskViewModel { get; set; } = new();
    private bool DisableSubmit = true;

    [Parameter] public string UserName { get; set; } = "";

    [Inject] private ITaskService TaskService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [CascadingParameter] private MudDialogInstance Dialog { get; set; } = null!;

    private void OnValidate(bool isValid)
    {
        DisableSubmit = !isValid;
        StateHasChanged();
    }

    private async Task CreateTask()
    {
        await TaskService.CreateTask(taskViewModel);
        if (taskViewModel.RowId != 0)
        {
            Dialog.Close(DialogResult.Ok(true));
        }
    }

    public void Cancel()
    {
        Dialog.Cancel();
    }
}

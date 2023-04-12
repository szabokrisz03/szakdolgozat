using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.MilestoneServices;
using TaskManager.Srv.Services.TaskServices;

namespace TaskManager.Srv.Components.Dialogs;

public partial class CreateMilestoneDialog
{
    private MilestoneViewModel milestoneViewModel { get; set; } = new();
    private bool DisableSubmit = true;

    [Parameter] public long TaskId { get; set; }

    [Inject] private IMilestoneService MilestoneService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [CascadingParameter] private MudDialogInstance Dialog { get; set; } = null!;

    private void OnValidate(bool isValid)
    {
        DisableSubmit = !isValid;
        StateHasChanged();
    }

    protected override void OnParametersSet()
    {
        milestoneViewModel.TaskId = TaskId;
        base.OnParametersSet();
    }

    private async Task CreateMilestone()
    {
        Dialog.Close(DialogResult.Ok(milestoneViewModel));
    }

    public void Cancel()
    {
        Dialog.Cancel();
    }
}

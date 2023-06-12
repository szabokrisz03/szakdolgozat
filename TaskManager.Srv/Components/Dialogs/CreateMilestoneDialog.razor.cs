using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.Dialogs;

public partial class CreateMilestoneDialog
{
	private MilestoneViewModel milestoneViewModel { get; set; } = new();
	private bool DisableSubmit = true;

	[Parameter] public long TaskId { get; set; }

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

	private void CreateMilestone()
	{
		Dialog.Close(DialogResult.Ok(milestoneViewModel));
	}

	public void Cancel()
	{
		Dialog.Cancel();
	}
}

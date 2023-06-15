using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

using System.Diagnostics.Eventing.Reader;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.MilestoneServices;
using TaskManager.Srv.Services.TaskServices;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Milestone
{
    [Parameter]
    public bool IsOpen { get; set; }
    private MudTable<MilestoneViewModel> milestoneTable;
    public List<MilestoneViewModel> milestoneList = new();
    [CascadingParameter(Name = "TaskId")] long Id { get; set; }
    [Inject] public IMilestoneService? milestoneService { get; set; } = null;
    [Inject] public IMilestoneViewService? MilestoneViewService { get; set; } = null;
    [Inject] private IDialogService DialogService { get; set; }

    private async Task CreateMilestone()
    {
        await MilestoneViewService!.CreateMilestoneDialog(Id);
        await ListMilestones();
        if (!IsOpen)
        {
            await ToggleDrawer();
        }

        if (milestoneTable != null)
        {
            await milestoneTable.ReloadServerData();
        }

    }

    private async Task ListMilestones()
    {
        milestoneList = await milestoneService!.ListMilestones(Id);
    }

    private async Task PopUpButton(MilestoneViewModel milestoneView) {
        if(milestoneView.Actual == null)
        {
            bool? result = await DialogService.ShowMessageBox(
            "Határidő lezárása", (MarkupString)$"Biztos lezárod a határidőt? <br /> A határidő lezárása nem vonható vissza!",
            yesText: "Lezárás", cancelText: "Mégse"
            );

            if (result != null)
            {
                await CloseMilestone(milestoneView.RowId);
                await ListMilestones();
                await milestoneTable.ReloadServerData();
            }

            StateHasChanged();
        }
    }

    private async Task DeletePopUpButton(MilestoneViewModel milestone)
    {
        bool? result = await DialogService.ShowMessageBox(
        "Határidő törlése", (MarkupString)$"Biztos törlöd a határidőt? <br /> A határidő törlése nem vonható vissza!",
        yesText: "Törlés", cancelText: "Mégse"
        );

        if (result != null)
        {
            await Delete(milestone.RowId);
            await ListMilestones();
            await milestoneTable.ReloadServerData();
        }

        StateHasChanged();
    }

    private async Task Delete(long milestoneId)
    {
        await milestoneService!.DeleteMilestone(milestoneId);
    }

    private async Task CloseMilestone(long milestoneId) {
        await milestoneService!.CloseMilestone(milestoneId);
    }

    private async Task ToggleDrawer()
    {
        await ListMilestones();
        IsOpen = !IsOpen;

    }
}

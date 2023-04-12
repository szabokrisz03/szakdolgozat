using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.MilestoneServices;
using TaskManager.Srv.Services.TaskServices;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Milestone
{
    [Parameter]
    public bool IsOpen { get; set; }
    public List<MilestoneViewModel> milestoneList = new();
    [CascadingParameter (Name = "TaskId")] long Id { get; set; }
    [Inject] public IMilestoneService? milestoneService { get; set; } = null;
    [Inject] public IMilestoneViewService? MilestoneViewService { get; set; } = null;

    private async Task CreateMilestone()
    {
        await MilestoneViewService!.CreateMilestoneDialog(Id);
        await ListMilestones();
    }

    private async Task ListMilestones()
    {
        milestoneList = await milestoneService!.ListMilestones(Id);
    }

    private void Update(MudItemDropInfo<MilestoneViewModel> info)
    {
        info.Item.Table = info.DropzoneIdentifier;
    }

    private async Task ToggleDrawer()
    {
        await ListMilestones();
        IsOpen = !IsOpen;

    }
}

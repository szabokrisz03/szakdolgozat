using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.MilestoneServices;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Milestone
{
    [Parameter]
    public bool IsOpen { get; set; }
    [Inject] public IMilestoneService milestoneService { get; set; }

    private List<MilestoneViewModel> MilestoneList = new()
    {
        new MilestoneViewModel() { Table="main", Date= DateTime.Now, Name="1. name"},
        new MilestoneViewModel() { Table="main", Date= DateTime.Now, Name="2. name"},
        new MilestoneViewModel() { Table="main", Date= DateTime.Now, Name="3. name"},
        new MilestoneViewModel() { Table="main", Date= DateTime.Now, Name="4. name"},
        new MilestoneViewModel() { Table="main", Date= DateTime.Now, Name="5. name"},
    };
      
    private void Update(MudItemDropInfo<MilestoneViewModel> info)
    {
        info.Item.Table = info.DropzoneIdentifier;
    }

    private void ToggleDrawer()
    {
        IsOpen = !IsOpen;
    }
}

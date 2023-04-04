using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Milestone
{
    [Parameter]
    public bool IsOpen { get; set; }

    private List<MilestoneViewModel> MilestoneList = new()
    {
        new MilestoneViewModel() { Id="main", Date= DateTime.Now, Name="1. name"},
        new MilestoneViewModel() { Id="main", Date= DateTime.Now, Name="2. name"},
        new MilestoneViewModel() { Id="main", Date= DateTime.Now, Name="3. name"},
        new MilestoneViewModel() { Id="main", Date= DateTime.Now, Name="4. name"},
        new MilestoneViewModel() { Id="main", Date= DateTime.Now, Name="5. name"},
    };
      
    private void Update(MudItemDropInfo<MilestoneViewModel> info)
    {
        info.Item.Id = info.DropzoneIdentifier;
    }

    private void ToggleDrawer()
    {
        IsOpen = !IsOpen;
    }
}

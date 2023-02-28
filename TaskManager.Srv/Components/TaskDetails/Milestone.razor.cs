using Microsoft.AspNetCore.Components;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Milestone
{
    [Parameter]
    public bool IsOpen { get; set; }

    private void ToggleDrawer()
    {
       IsOpen= !IsOpen;
    }
}

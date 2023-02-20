namespace TaskManager.Srv.Components.TaskDetails;

public partial class Milestone
{
    public bool IsOpen { get; set; }

    private void ToggleDrawer()
    {
        IsOpen= !IsOpen;
    }
}

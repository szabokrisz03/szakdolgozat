namespace TaskManager.Srv.Model.ViewModel;

public class TaskViewModel
{
    public long RowId { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string State { get; set; } = "";
    public bool ShowDetails { get; set; }
}

namespace TaskManager.Srv.Model.ViewModel;

public class TaskViewModel
{
    public long ProjectId { get; set; }
    public long RowId { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string State { get; set; } = "";
}

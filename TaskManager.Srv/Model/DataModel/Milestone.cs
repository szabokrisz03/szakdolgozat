namespace TaskManager.Srv.Model.DataModel;

public class Milestone: DbTable
{
    public long TaskId { get; set; }
    public string Name { get; set; } = "";
    public DateTime Deadline { get; set; }

    public Project Project { get; set; }
}

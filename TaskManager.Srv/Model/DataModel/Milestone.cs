namespace TaskManager.Srv.Model.DataModel;

public class Milestone: DbTable
{
    public long ProjectId { get; set; }
    public string Name { get; set; } = "";

    public Project Project { get; set; }
}

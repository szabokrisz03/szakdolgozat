namespace TaskManager.Srv.Model.DataModel;

public class ProjectTask: DbTable
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public Guid TechnicalName { get; set; }
    public TaskState State { get; set; }

    public ICollection<TaskMilestone> Milestones { get; set; }
}

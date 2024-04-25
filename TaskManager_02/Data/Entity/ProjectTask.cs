#nullable enable

namespace TaskManager_02.Data.Entity;

public class ProjectTask
{
    public long RowId { get; set; }
    public long ProjectId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public int Priority { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public Project Project { get; set; }
}

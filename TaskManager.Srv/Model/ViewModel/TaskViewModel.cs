using TaskManager.Srv.Model.DataModel;

namespace TaskManager.Srv.Model.ViewModel;

/// <summary>
/// Feladatot ábrázoló modell
/// </summary>
public class TaskViewModel
{
    public long ProjectId { get; set; }
    public long RowId { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public TaskState State { get; set; }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Model.DataModel;

public class Project: DbTable
{
    public string Name { get; set; } = "";
    public Guid TechnicalName { get; set; }
    
    public ICollection<ProjectUser> ProjectUsers { get; set; }
    public ICollection<WiLinkTemplate> WiLinkTemplates { get; set; }
    public ICollection<ProjectTask> ProjectTasks { get; set; }
}

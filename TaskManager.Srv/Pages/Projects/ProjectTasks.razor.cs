using Microsoft.AspNetCore.Components;

namespace TaskManager.Srv.Pages.Projects;

public partial class ProjectTasks
{
    [Parameter] public string TechnicalName { get; set; } = "";
}

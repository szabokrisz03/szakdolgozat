using Microsoft.AspNetCore.Components;

namespace TaskManager_02.Components.Pages;

public partial class ProjectTasks
{
    [Parameter] public string TechnicalName { get; set; } = "";

}

using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.TaskServices;
using TaskManager.Srv.Services.WiLinkService;

namespace TaskManager.Srv.Pages.Projects;

public partial class ProjectTasks
{

    private string infoFormat = "{first_item}-{last_item}";
    private long ShownId = 0;
    private ProjectViewModel project = new();
    [Parameter] public string TechnicalName { get; set; } = "";
    [Parameter] public int PageSize { get; set; }

    [Inject] private ITaskViewService TaskViewService { get; set; } = null!;
    [Inject] private ITaskService TaskService { get; set; } = null!;

    protected override void OnInitialized()
    {
        PageSize = 8;
    }

    private async Task CreateTask()
    {
        await TaskViewService.CreateTaskDialog();
    }

    private async Task<TableData<TaskViewModel>> LoadData(TableState state)
    {
        int skip = state.PageSize * state.Page;
        int take = state.PageSize;

        int size = await TaskService.CountTasks(project.RowId);
        var tasks = await TaskService.ListTasks(project.RowId, take, skip);
        ShownId = 0;

        return new TableData<TaskViewModel>
        {
            Items = tasks,
            TotalItems = size
        };
    }

    private void ShowBtnPress(TaskViewModel taskView)
    {
        ShownId = ShownId == taskView.RowId ? 0 : taskView.RowId;
    }
}

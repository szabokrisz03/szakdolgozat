using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.ProjectServices;
using TaskManager.Srv.Services.TaskServices;
using TaskManager.Srv.Services.WiLinkService;

namespace TaskManager.Srv.Pages.Projects;

public partial class ProjectTasks
{

    private string infoFormat = "{first_item}-{last_item}";
    private long ShownId = 0;
    private MudTable<TaskViewModel> _table;

    [Parameter] public string TechnicalName { get; set; } = "";
    [Parameter] public int PageSize { get; set; }

    [Inject] private ITaskViewService TaskViewService { get; set; } = null!;
    [Inject] private ITaskService TaskService { get; set; } = null!;
    [Inject] private IProjectDisplayService project { get; set; } = null!;

    private Guid _lastTechnicalName;
    private long _projectId = 0;

    protected override void OnInitialized()
    {
        PageSize = 10;
    }

    protected override async Task OnParametersSetAsync()
    {
        Guid.TryParse(TechnicalName, out Guid technicalName);
        if (_lastTechnicalName != technicalName)
        {
            _lastTechnicalName = technicalName;
            _projectId = await project.GetProjectIdAsync(technicalName);
        }
    }

    private async Task CreateTask()
    {
        await TaskViewService.CreateTaskDialog(TechnicalName);
        await _table.ReloadServerData();
    }

    private async Task<TableData<TaskViewModel>> LoadData(TableState state)
    {
        int skip = state.PageSize * state.Page;
        int take = state.PageSize;

        int size = await TaskService.CountTasks(_projectId);
        var tasks = await TaskService.ListTasks(_projectId, take, skip);
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

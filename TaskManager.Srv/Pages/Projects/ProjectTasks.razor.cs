using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

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

    [CascadingParameter] long Id { get; set; }

    [Parameter] public string TechnicalName { get; set; } = "";
    [Parameter] public int PageSize { get; set; }

    [Inject] private ITaskViewService TaskViewService { get; set; } = null!;
    [Inject] private ITaskService TaskService { get; set; } = null!;
    [Inject] private IProjectDisplayService project { get; set; } = null!;

    private List<(TaskState value, string name)> enumList = new();
    private Guid _lastTechnicalName;
    private long _projectId = 0;

    protected override void OnInitialized()
    {
        PageSize = 10;
        ListTaskState();
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

	/// <summary>
	/// Feladat létrehozása
	/// </summary>
	private async Task CreateTask()
    {
        await TaskViewService.CreateTaskDialog(TechnicalName);
        await _table.ReloadServerData();
    }

	/// <summary>
	/// Táblázat feltöltése
	/// </summary>
	/// <param name="state">TableState</param>
	/// <returns>TableData<TaskViewModel></returns>
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

	/// <summary>
	/// Feladat státuszának frissítése.
	/// </summary>
	/// <param name="model">A frissítendő feladat</param>
	private async Task UpdateState(TaskViewModel model)
    {
        await TaskService.UpdateStatus(model);
    }

	/// <summary>
	/// Feladat státuszainak beállítása.
	/// </summary>
	private void ListTaskState()
    {
        var type = typeof(TaskState);

        enumList = Enum.GetValues(typeof(TaskState))
            .Cast<TaskState>()
            .Select(v =>
            {
                string valueName = v.ToString();
                string name;
                name = type.GetMember(valueName).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>()?.Name ?? valueName;

                return (v, name);
            }).ToList();
    }

	/// <summary>
	/// Lenyíló menüt szabályozza.
	/// </summary>
	/// <param name="taskView">A lenyíló feladat</param>
	private void ShowBtnPress(TaskViewModel taskView)
    {
        Id = taskView.RowId;
        ShownId = ShownId == taskView.RowId ? 0 : taskView.RowId;   
    }
}

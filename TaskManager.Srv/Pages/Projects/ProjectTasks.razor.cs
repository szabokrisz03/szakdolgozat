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
    [Parameter] public string TechnicalName { get; set; } = "";
    [Parameter] public int PageSize { get; set; }

    [Inject] private ITaskViewService TaskViewService { get; set; } = null!;

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

        var tasks = new List<TaskViewModel>();
        for (int i = 0; i < 12; i++)
        {
            tasks.Add(new TaskViewModel
            {
                RowId = i + 1,
                Name = "teszt1",
                State = "folyamatban",
                Description = "alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma"
            });
        }

        var size = tasks.Count();

        //page váltáskor bezárja az adott oldalon megnyitott desc blokkot
        ShownId = 0;
        return new TableData<TaskViewModel>
        {
            Items = tasks.Skip(skip).Take(take),
            TotalItems = size
        };
    }

    private void ShowBtnPress(TaskViewModel taskView)
    {
        ShownId = ShownId == taskView.RowId ? 0 : taskView.RowId;
    }
}

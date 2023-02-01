using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Pages.Projects;

public partial class ProjectTasks
{
    [Parameter] public string TechnicalName { get; set; } = "";

    private async Task<TableData<TaskViewModel>> LoadData(TableState state)
    {
        int skip = state.PageSize * state.Page;
        int take = state.PageSize;

        var tasks = new List<TaskViewModel>();
        var size = tasks.Count();

        return new TableData<TaskViewModel>
        {
            Items = tasks.Skip(skip).Take(take),
            TotalItems = size
        };
    }
}

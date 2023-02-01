using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Pages.Projects;

public partial class ProjectTasks
{
    private MudTable<TaskViewModel> _table;
    private long ShownId = 0;
    [Parameter] public string TechnicalName { get; set; } = "";

    private async Task<TableData<TaskViewModel>> LoadData(TableState state)
    {
        int skip = state.PageSize * state.Page;
        int take = state.PageSize;

        var tasks = new List<TaskViewModel>();
        for(int i = 0; i < 12; i++)
        {
            tasks.Add(new TaskViewModel
            {
                RowId = i + 1,
                Name = "teszt1",
                State = "folyamatban",
                Description = "alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma alma "
            }); 
        }

        var size = tasks.Count();

        return new TableData<TaskViewModel>
        {
            Items = tasks.Skip(skip).Take(take),
            TotalItems = size
        };
    }

    private async Task PageChanged(int i)
    {
        _table.NavigateTo(i - 1);
        
    }

    private void ShowBtnPress(TaskViewModel taskView)
    {
        if (ShownId == taskView.RowId)
        {
            ShownId = 0;
        }
        else
        {
            ShownId = taskView.RowId;
        }
    }
}

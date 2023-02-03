using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;

using MudBlazor;

using System.Collections.Specialized;
using System.Web;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Pages.Projects;

public partial class ProjectTasks
{
    private MudTable<TaskViewModel>? _table;
    private long ShownId = 0;

    [Inject] public NavigationManager NavManager { get; set; } = null!;

    [Parameter] public string TechnicalName { get; set; } = "";

    [Parameter]
    [SupplyParameterFromQuery(Name = "page_index")]
    public int PageIndex { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "page_size")]
    public int PageSize { get; set; }

    private void SetRouting()
    {
        var uriBuilder = new UriBuilder(NavManager.Uri);
        string newQuery = $"{uriBuilder.Path}?page_size={PageSize}&page_index={PageIndex}";
        NavManager.NavigateTo(newQuery, forceLoad: false, replace: true);
    }

    protected override void OnInitialized()
    {
        if (PageSize == 0)
        {
            PageSize = 9;
        }

        SetRouting();
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

        return new TableData<TaskViewModel>
        {
            Items = tasks.Skip(skip).Take(take),
            TotalItems = size
        };
    }

    private void PageChanged(int i)
    {
        PageIndex = i - 1;
        SetRouting();
    }

    private void ShowBtnPress(TaskViewModel taskView)
    {
        ShownId = ShownId == taskView.RowId ? 0 : taskView.RowId;
    }
}

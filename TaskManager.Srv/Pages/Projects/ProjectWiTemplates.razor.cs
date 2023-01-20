using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.ProjectServices;
using TaskManager.Srv.Services.WiLinkService;

namespace TaskManager.Srv.Pages.Projects;

public partial class ProjectWiTemplates
{
    [Parameter] public string TechnicalName { get; set; } = "";
    [Inject] private IProjectDisplayService ProjectDisplayService { get; set; }
    [Inject] private IWiLinkTemplateService DataService { get; set; }

    private ProjectViewModel project = new();

    protected override async Task OnParametersSetAsync()
    {
        project.RowId = 0;
        if (TechnicalName != null && Guid.TryParse(TechnicalName, out Guid technicalNameGuid))
        {
            project = await ProjectDisplayService.GetProjectAsync(technicalNameGuid) ?? new();
        }
    }

    private async Task<TableData<WiLinkTemplateViewModel>> LoadData(TableState state)
    {
        int skip = state.PageSize * state.Page;
        int take = state.PageSize;

        int size = await DataService.CountTemplates(project.RowId);
        var templates = await DataService.ListTemplates(project.RowId, take, skip);

        return new TableData<WiLinkTemplateViewModel>
        {
            Items = templates,
            TotalItems = size
        };
    }
}

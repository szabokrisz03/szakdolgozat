using MudBlazor;

using TaskManager_02.Components.Dialogs;

namespace TaskManager_02.Services.ProjectServices;

public class ProjectViewService : IProjectViewService
{
    private readonly IDialogService _dialogService;

    public ProjectViewService(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task OpenProjectDialogAsync(string userName)
    {
        var parameters = new DialogParameters
        {
            ["UserName"] = userName,
        };

        var dialog = await _dialogService.ShowAsync<ProjectDialog>("Projekt hozzádása", parameters);
        await dialog.Result;
    } 
}

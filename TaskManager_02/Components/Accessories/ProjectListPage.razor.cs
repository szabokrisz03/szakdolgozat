using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

using TaskManager_02.Data.ViewModels;
using TaskManager_02.Services.ProjectServices;
using TaskManager_02.Services.UserServices;
using MudBlazor;

namespace TaskManager_02.Components.Accessories;

public partial class ProjectListPage
{
    [Inject] private IProjectService ProjectService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [CascadingParameter] private Task<AuthenticationState>? authenticationStateTask { get; set; }
    [Parameter] public bool isMyProject { get; set; } = false;

    private MudDataGrid<ProjectViewModel> projectGrid = default!;
    private List<ProjectViewModel> projects = new();
    private string authenticatedUserName = "";
    private UserViewModel userViewModel = new();

    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationStateTask!;
        authenticatedUserName = authState.User.Identity?.Name ?? "";
        userViewModel = await UserService.GetUser(authenticatedUserName);
    }

    protected override async Task OnParametersSetAsync()
    {
        var list = await ListProjectsAsync();
        await projectGrid.ReloadServerData();
    }

    private async Task<List<ProjectViewModel>> ListProjectsAsync()
    {
        return projects = isMyProject
            ? await ProjectService.ListUserProjectsAsync(userViewModel)
            : await ProjectService.ListProjectAsync();
    }

    private void OpenProjectTasks(ProjectViewModel projectViewModel)
    {
        NavigationManager.NavigateTo($"projects/{projectViewModel.TechnicalName}/tasks");
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.ProjectServices;

namespace TaskManager.Srv.Shared;

public partial class NavMenu: IDisposable
{
    private string _user;

    [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IProjectDisplayService ProjectDisplayService { get; set; }

    private Guid? projectTechnicalName;
    private string projectName = "Projekt";

    protected override async Task OnInitializedAsync()
    {
        if (authenticationStateTask != null)
        {
            var authstate = await authenticationStateTask;
            _user = authstate.User.Identity?.Name ?? "";
        }

        await LocationChangedAsync();
        NavigationManager.LocationChanged += LocationChanged!;
    }

    protected override async Task OnParametersSetAsync()
    {
        await LocationChangedAsync();
    }

    void LocationChanged(object sender, LocationChangedEventArgs e)
    {
        base.InvokeAsync(LocationChangedAsync);
    }

    private async Task LocationChangedAsync()
    {
        var uriComps = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Split('/');

        if (uriComps.Length == 3 && uriComps[0] == "projects")
        {
            projectTechnicalName = Guid.TryParse(uriComps[1], out Guid technicalName) ? technicalName : null;
            projectName = await ProjectDisplayService.GetProjectNameAsync(projectTechnicalName.GetValueOrDefault()) ?? "Projekt";
        }

        StateHasChanged();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= LocationChanged!;
    }
}

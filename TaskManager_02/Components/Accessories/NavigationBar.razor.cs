using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

using MudBlazor;
using TaskManager_02.Services.ProjectServices;

namespace TaskManager_02.Components.Accessories;

public partial class NavigationBar
{
    [Inject] private IProjectViewService ProjectViewService { get; set; } = default!;
    [CascadingParameter] private MudDialogInstance DialogInstance { get; set; } = default!;
    [CascadingParameter] private Task<AuthenticationState>? authenticationStateTask { get; set; }

    private string authenticatedUserName = "";

    /// <summary>
    /// 
    ///     A bejelentkezett felhasználó adatainak a lekérése.
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationStateTask!;
        authenticatedUserName = authState.User.Identity?.Name ?? "";
    }

    /// <summary>
    /// 
    ///     A projekt létrehozásához szükséges dialógus betöltése.
    /// 
    /// </summary>
    private async void OpenProjectDialog()
    {
        await ProjectViewService.OpenProjectDialogAsync(authenticatedUserName);
    }
}

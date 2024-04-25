using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace TaskManager_02.Components.Pages;

/// <summary>
/// 
///     Hibakezelésért felelős osztály.
/// 
/// </summary>
public partial class Error
{
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///     Kiírja a hiba üzenetet a felhasználó számára.
    /// </summary>
    /// <param name="exception"> A keletkezett kivétel </param>
    public void ErrorHandler(Exception exception)
    {
        Snackbar.Add(exception.Message, Severity.Error);
        StateHasChanged();
    }
}

using FluentValidation;

using Microsoft.AspNetCore.Components;
using MudBlazor;

using TaskManager_02.Components.Pages;
using TaskManager_02.Data.ViewModels;
using TaskManager_02.Extensions.Exceptions;
using TaskManager_02.Services.ProjectServices;
using TaskManager_02.Services.UserServices;

namespace TaskManager_02.Components.Dialogs;

public partial class ProjectDialog
{
    [Inject] IValidator<ProjectViewModel> ValidationRules { get; set; } = default!;
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IProjectService ProjectService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
    [CascadingParameter] public Error TaskManagerError { get; set; } = default!;
    [Parameter] public string UserName { get; set; } = "";
    [Parameter] public ProjectViewModel projectViewModel { get; set; } = new();

    /// <summary>
    /// 
    ///     Létrehozza a projektet.
    ///     Ha sikeres a hozzáadás -> Snackbaron megjelenik a "Sikeres hozzáadás!" üzenet.
    ///     Ha nem sikeres a hozzáadás -> Snackbaron megjelenik a hiba oka.
    /// 
    /// </summary>
    private async Task SubmitProject()
    {
        try
        {
            var user = await UserService.GetUser(UserName);
            bool result = await ProjectService.CreateProjectAsync(projectViewModel, user);
            if ( result )
            {
                Snackbar.Add("Sikeres hozzáadás!", MudBlazor.Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
                MudDialog.StateHasChanged();

            }
        }
        catch (Exception ex) when (
            ex is TaskManagerException ||
            ex is OperationCanceledException)
        {
            TaskManagerError.ErrorHandler(ex);
            return;
        }
    }

    /// <summary>
    /// 
    ///     Dialógus ablak bezárása.
    /// 
    /// </summary>
    private void Close() => MudDialog.Cancel();
}

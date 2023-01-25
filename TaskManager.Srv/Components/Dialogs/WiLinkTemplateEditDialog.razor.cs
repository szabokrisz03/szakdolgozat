using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Components.Forms;
using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.WiLinkService;

namespace TaskManager.Srv.Components.Dialogs;

public partial class WiLinkTemplateEditDialog
{
    private bool _disableSubmit = false;
    private WiLinkTemplateForm? Form;

    [Parameter] public WiLinkTemplateViewModel ViewModel { get; set; } = new();

    [CascadingParameter] private MudDialogInstance Dialog { get; set; } = null!;

    [Inject] public IWiLinkTemplateService TemplateService { get; set; } = null!;

    private void OnValidate(bool isValid)
    {
        _disableSubmit = !isValid;
        StateHasChanged();
    }

    private void Cancel()
    {
        Dialog.Cancel();
    }

    private async Task EditTemplate()
    {
        if (ViewModel.RowId== 0)
        {
            await CreateTemplate();
        }
        else
        {
            await UpdateTemplate();
        }
    }

    private async Task CreateTemplate()
    {
        await TemplateService.CreateTemplate(ViewModel);
    }

    private async Task UpdateTemplate()
    {
        await TemplateService.UpdateTemplate(ViewModel);
    }
}

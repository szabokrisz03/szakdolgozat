using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.WiLinkService;

public interface IWiLinkTemplateViewService
{
    Task CreateTemplateDialog();
    Task UpdateTemplateDialog(WiLinkTemplateViewModel viewModel);
}

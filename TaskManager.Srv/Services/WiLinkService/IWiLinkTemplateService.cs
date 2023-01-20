using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.WiLinkService;

public interface IWiLinkTemplateService
{
    Task<List<WiLinkTemplateViewModel>> ListTemplates(long projectId, int take = 10, int skip = 0);
    Task<int> CountTemplates(long projectId);
    Task CreateTemplate(WiLinkTemplateViewModel template);
    Task UpdateTemplate(WiLinkTemplateViewModel template);
    Task DeleteTemplate(long templateId);
}

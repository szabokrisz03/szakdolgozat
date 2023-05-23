using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.WiServices;

public interface IWiService
{
	Task<WiViewModel> CreateWiAsync(WiViewModel wiViewModel);
	Task<List<WiViewModel>> ListWorkItem(long taskId);
}

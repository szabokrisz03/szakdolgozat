using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.WiServices;

public interface IWiService
{
    Task CreateWiAsync(int wiId, long taskId);
    Task<int[]> ListWorkItem(long taskId);
}

using TaskManager.Srv.Model.DTO;

namespace TaskManager.Srv.Services.WiServices;

public interface IWiStateService
{
    void PropertyWis(IEnumerable<int> source, List<WorkItem> collector);
    List<WorkItem> DetailWIs(IEnumerable<int> sources, int capacity = 0);
    List<WorkItem> ListConnectingWis(int wiId);
    Dictionary<int, List<WorkItem>> queryMaker(IEnumerable<int> source);
}

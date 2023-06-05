using TaskManager.Srv.Model.DTO;

namespace TaskManager.Srv.Services.WiServices;

public interface IWiStateService
{
	HashSet<int> ListWIs(int wiId);
	void PropertyWis(IEnumerable<int> source, List<WorkItem> collector);
	List<WorkItem> DetailWIs(IEnumerable<int> sources, int capacity = 0);
	HashSet<int> ListConnectingWis(int wiId);
}

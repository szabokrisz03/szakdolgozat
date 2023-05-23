using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TaskManager.Srv.Services.WiServices;

public interface IWiStateService
{
	Task<IList<WorkItem>> GetWorkItem(IEnumerable<int> wiId);
}

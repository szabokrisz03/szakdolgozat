using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;

using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

using static MudBlazor.CategoryTypes;

namespace TaskManager.Srv.Services.WiServices;

public class WiStateService : IWiStateService
{

	private readonly IDbContextFactory<ManagerContext> dbContextFactory;
	private readonly IMapper mapper;
	private Uri baseUri;
	private string accessToken;
	private List<WiViewModel> wiList;
	private List<IEnumerable<int>> wiIdList;

	public WiStateService(
		IDbContextFactory<ManagerContext> dbContextFactory,
		IMapper mapper)
	{
		this.dbContextFactory = dbContextFactory;
		this.mapper = mapper;
		wiList = new List<WiViewModel>();
		wiIdList = new List<IEnumerable<int>>();
	}

	public async Task<IList<WorkItem>> GetWorkItem(IEnumerable<int> wiId)
	{
		accessToken = "7q6dds4qvedqwoow3wf7flhk2jnohqx4fv5uri3banermcbxjuma";
		baseUri = new Uri("https://azdoapp.griffsoft.hu/tfs/Test");
		var credentials = new VssBasicCredential(string.Empty, accessToken);
		List<WorkItem> item = new();

		foreach(var wi in wiId)	
		{
			var wiql = new Wiql()
			{
				Query = "Select * " +
				"From WorkItems " +
				"Where [System.Id] = " + wi + " " +
				"And [System.TeamProject] = 't_taskmanager' " +
				"And [System.State] <> 'Closed' " +
				"Order By [State] Asc, [Changed Date] Desc",
			};

			using (var httpClient = new WorkItemTrackingHttpClient(baseUri, credentials))
			{
				WorkItemQueryResult workItemQueryResult = httpClient.QueryByWiqlAsync(wiql).Result;

				List<int> list = new();
				foreach (var items in workItemQueryResult.WorkItems)
				{
					list.Add(items.Id);
				}

				if (list.Count > 0)
				{
					int[] arr = list.ToArray();
					string[] fields = new string[5];
					fields[0] = "System.Id";
					fields[1] = "System.Title";
					fields[2] = "System.State";
					fields[3] = "System.WorkItemType";
					fields[4] = "System.ChangedDate";

					var workItems = httpClient.GetWorkItemsAsync(arr, fields, workItemQueryResult.AsOf).Result;

					item.Add(workItems.First());

				}
			}
		}

		return item;
	}
}

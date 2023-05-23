using Microsoft.AspNetCore.Components;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

using MudBlazor;

using Newtonsoft.Json.Linq;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.WiServices;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class ConnectingWi
{

	[CascadingParameter(Name = "TaskId")] long Id { get; set; }
	[Parameter] public int? WiNumber { get; set; }
	[Inject] public IWiService? wiService { get; set; }
	[Inject] public IWiStateService? wiStateService { get; set; }

	private List<WiViewModel> wiViewModels = new();
	private List<WiViewModel> wiFinalList = new();
	private MudNumericField<int?> _numField;

	protected override async Task OnInitializedAsync()
	{
		await ListWi();
	}

	public async Task ListWi()
	{
		wiViewModels = await wiService!.ListWorkItem(Id);
		int[] wiArray = wiViewModels.Select(p => p.WiId!.Value).ToArray();
		var workitems = await wiStateService!.GetWorkItem(wiArray);
		wiFinalList = setWorkItemProprty(workitems);
		Console.WriteLine();
	}

	public List<WiViewModel> setWorkItemProprty(IList<WorkItem> wi)
	{
		List<WiViewModel> wiList = new();

		if(wi != null)
		{
			foreach(var item in wi)
			{
				wiList.Add(new WiViewModel
				{
					WiId = item.Id,
					TaskId = Id,
					WiState = item.Fields["System.State"].ToString()!,
					WiName = item.Fields["System.Title"].ToString()!,
					WiDate = item.Fields["System.ChangedDate"].ToString()!,
				});
			}
		}

		return wiList;
	}

    public async Task AddWi()
    {
        if(WiNumber != null)
        {
			foreach (var item in wiFinalList)
			{
				if (WiNumber == item.WiId)
				{
					_numField.Reset();
					return;
				}
			}

			int[] value = new int[] { WiNumber.Value };
			var workItems = await wiStateService!.GetWorkItem(value);
			var wi = setWorkItemProprty(workItems);
			wiFinalList.Add(wi.First());
			WiViewModel wiViewModel = wiFinalList.Find(p => p.WiId == WiNumber)!;
			await wiService!.CreateWiAsync(wiViewModel);
		}

		_numField.Reset();
    }
}
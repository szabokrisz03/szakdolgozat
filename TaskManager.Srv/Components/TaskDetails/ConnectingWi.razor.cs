using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.DTO;
using TaskManager.Srv.Services.WiServices;

using WhExportShared.Exceptions;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class ConnectingWi
{

	[CascadingParameter(Name = "TaskId")] private long Id { get; set; }
	[Parameter] public int? WiNumber { get; set; }
	[Inject] public IWiService? WiService { get; set; }
	[Inject] public IWiStateService? WiStateService { get; set; }

	private int[]? wiIdArray;
	private List<WorkItem>? workItems;
	private List<WorkItem>? workItemChildrens;
	private Dictionary<int, List<WorkItem>>? wiDetails;
	private MudNumericField<int?>? _numField;

	protected override async Task OnInitializedAsync()
	{
		await ListWis();
	}

	public async Task ListWis()
	{
		wiIdArray = await WiService!.ListWorkItem(Id);
		wiDetails = WiStateService!.queryMaker(wiIdArray);

		foreach (var wiList in wiDetails.Values)
		{
			wiList.Sort();
		}

		workItems = WiStateService!.DetailWIs(wiIdArray, wiIdArray.Length);

		foreach (var item in workItems)
		{
			var childrens = GetWorkItemChildrens(item.Id);
			if (childrens.Count != 0)
			{
				var pointerWi = GetChildrenWi(childrens);
				WiStateNameChanger(pointerWi, item);
			}
		}
	}

	public List<WorkItem> GetWorkItemChildrens(int wiId)
	{
		workItemChildrens = new List<WorkItem>();

		foreach (var item in wiDetails!)
		{
			if (item.Key == wiId)
			{
				foreach (var child in item.Value)
				{
					workItemChildrens.Add(child);
				}

				workItemChildrens.Sort();
			}
		}

		return workItemChildrens;
	}

	public WorkItem GetChildrenWi(List<WorkItem> sortedWis)
	{
		var wi = sortedWis.Where(x => x.State == "In Progress").LastOrDefault();

		if (wi is null)
		{
			var lastDone = sortedWis.Where(x => x.State == "Done").LastOrDefault();

			wi = lastDone != null
				? sortedWis.Where(x => (x.State == "To Do" || x.State == "New") && (x.CompareTo(lastDone) > 0)).FirstOrDefault()
				: sortedWis.Where(x => x.State == "To Do" || x.State == "New").FirstOrDefault();

			wi ??= sortedWis.Where(x => x.State == "Done").LastOrDefault();
		}

		return wi is null ? throw new NonFatalException("Nem lett lekezelve az összes státusz!") : wi;
	}

	public void WiStateNameChanger(WorkItem wi, WorkItem item)
	{
		if (wi.Type == "Rendszerszervezési feladat")
		{
			switch (wi.State)
			{
				case "To Do":
					item.ClearState = "Specifikációra vár";
					break;
				case "In Progress":
					item.ClearState = "Specifikáció alatt";
					break;
				case "Done":
					item.ClearState = "Specifikáció kész";
					break;
			}
		}

		if (wi.Type == "Fejlesztési feladat")
		{
			switch (wi.State)
			{
				case "To Do":
					item.ClearState = "Fejlesztésre vár";
					break;
				case "In Progress":
					item.ClearState = "Fejlesztés alatt";
					break;
				case "Done":
					item.ClearState = "Fejlesztés kész";
					break;
			}
		}

		if (wi.Type == "Technikai változásjelentés")
		{
			switch (wi.State)
			{
				case "To Do":
					item.ClearState = "Tesztelésre vár";
					break;
				case "In Progress":
					item.ClearState = "Tesztelés alatt";
					break;
				case "Kiadásra vár":
					item.ClearState = "Kiadásra vár";
					break;
				case "Done":
					item.ClearState = "Kiadva";
					break;
			}
		}

		if (wi.Type == "Hibajegy")
		{
			switch (wi.State)
			{
				case "New":
					item.ClearState = "Hibajavításra vár";
					break;
				case "In Progress":
					item.ClearState = "Hibajavítás alatt";
					break;
				case "Done":
					item.ClearState = "Hibajavítás kész";
					break;
			}
		}
	}

	public async Task AddWi()
	{
		if (WiNumber != null)
		{
			foreach (var id in wiIdArray!)
			{
				if (WiNumber == id)
				{
					_numField!.Reset();
					return;
				}
			}

			workItemChildrens = WiStateService?.ListConnectingWis(WiNumber.Value);

			if (workItemChildrens?.Count <= 0)
			{
				_numField!.Reset();
				return;
			}

			WiService?.CreateWiAsync(WiNumber.Value, Id);
			await ListWis();
		}

		_numField!.Reset();
	}

	public void ShowConnectingWi(int id)
	{
		if (workItemChildrens != null)
		{
			workItemChildrens = new();
		}

		GetWorkItemChildrens(id);

		foreach (var item in workItems!)
		{
			item.IsOpen = item.Id == id && !item.IsOpen;
		}
	}
}
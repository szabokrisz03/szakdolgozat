using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.DTO;
using TaskManager.Srv.Services.WiServices;

using WhExportShared.Exceptions;

namespace TaskManager.Srv.Components.TaskDetails;

/// <summary>
/// Workitemek megjelenítése.
/// </summary>
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

	/// <summary>
	/// Workitemek listázása.
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// linkelt workitemek listázása.
	/// </summary>
	/// <param name="wiId">Workitem egyedi azonosítója</param>
	/// <returns>linkelt workitemeket tartalmazó lista</returns>
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

	/// <summary>
	/// Sorbarendezett workitemek közül adja vissza a státusz szerint a legkedvezőbbet az alábbi szerint:
	/// - Sorrendben a legkésőbbi "InProgress"
	/// - Ha nincs, akkor a legutolsó "Done" utáni első "ToDo"
	/// - Ha nincs, a legutolsó "Done"
	/// </summary>
	/// <param name="sortedWis">Sorbarendezett workitemeket tartalmazó lista</param>
	/// <returns>A megtalált workitem</returns>
	/// <exception cref="NonFatalException"></exception>
	public WorkItem GetChildrenWi(List<WorkItem> sortedWis)
    {
        var wi = sortedWis.Where(x => x.State == "In Progress").LastOrDefault();

        if (wi is null)
        {
            var lastDone = sortedWis.Where(x => x.State == "Done").LastOrDefault();

            wi = lastDone != null
                ? sortedWis.Where(x => (x.State == "To Do" || x.State == "New") && (x.CompareTo(lastDone) > 0)).FirstOrDefault()
                : sortedWis.Where(x => (x.State == "To Do" || x.State == "New")).FirstOrDefault();

            wi ??= sortedWis.Where(x => x.State == "Done").LastOrDefault();
        }

        return wi is null ? throw new NonFatalException("Nem lett lekezelve az összes státusz!") : wi;
    }

	/// <summary>
	/// Típus és státusz szerint nevezi át a workitemek státuszát.
	/// </summary>
	/// <param name="wi">A linkelt workitem</param>
	/// <param name="item">A változtatandó workitem</param>
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

	/// <summary>
	/// Workitem hozzáadása.
	/// </summary>
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

	/// <summary>
	/// Lenyíló menüért felelős.
	/// </summary>
	/// <param name="id">A lenyitandó workitem</param>
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
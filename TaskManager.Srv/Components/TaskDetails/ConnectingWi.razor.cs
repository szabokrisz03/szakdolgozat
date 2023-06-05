using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.DTO;
using TaskManager.Srv.Services.WiServices;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class ConnectingWi
{

	[CascadingParameter(Name = "TaskId")] private long Id { get; set; }
	[Parameter] public int? WiNumber { get; set; }
	[Inject] public IWiService? wiService { get; set; }
	[Inject] public IWiStateService? wiStateService { get; set; }

	private int[] wiIdArray;
	private List<WorkItem>? workItems;
	private MudNumericField<int?> _numField;

	protected override async Task OnInitializedAsync()
	{
		await ListWis();
	}

	public async Task ListWis()
	{
		wiIdArray = await wiService!.ListWorkItem(Id);
		workItems = wiStateService!.DetailWIs(wiIdArray, 0);
	}

	public async Task AddWi()
	{
		if (WiNumber != null)
		{
			foreach (var id in wiIdArray)
			{
				if (WiNumber == id)
				{
					_numField.Reset();
					return;
				}
			}

			var wisLoaded = wiStateService?.ListWIs(WiNumber.Value);

			if (wisLoaded?.Count <= 0)
			{
				_numField.Reset();
				return;
			}

			var wi = wiStateService.ListConnectingWis(WiNumber.Value);
			var wiis = wiStateService!.DetailWIs(wi, 0);
			wiService?.CreateWiAsync(WiNumber.Value, Id);
			await ListWis();

		}

		_numField.Reset();
	}
}
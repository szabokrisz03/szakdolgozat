using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class ConnectingWi
{

    [CascadingParameter(Name = "RowId")] long Id { get; set; }
    [Parameter] public long? WiNumber { get; set; }

    private List<ConnectingWiViewModell> connectingWiViewModells = new();
    private MudNumericField<long?> _numField;

    public void addWi()
    {
        if(WiNumber != null)
        {
            ConnectingWiViewModell con = new()
            {
                WiId = WiNumber!.Value,
                Title = "tesztName",
                TaskId = Id,
                Status = "folyamatban",
                AssignedTo = "szabokd",
                LastUpdate = DateTime.Now,
            };
            connectingWiViewModells.Add(con);
        }
        _numField.Reset();
    }
}

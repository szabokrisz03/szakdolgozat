using Microsoft.AspNetCore.Components;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Filter
{

    private bool isOpen = false;
    [Parameter] public StateFilterViewModell? StateFilterViewModell { get; set; }

    protected override void OnInitialized()
    {
        StateFilterViewModell = new StateFilterViewModell();
    }

    private void FilterOpenChanged()
    {
        isOpen = !isOpen;
    }
}

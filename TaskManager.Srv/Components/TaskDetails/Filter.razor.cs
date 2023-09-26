using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using TaskManager.Srv.Migrations;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Filter
{

    private bool isOpen = false;
    private MudTable<TaskViewModel> _table;
    private List<(PropertyInfo, string)> filterList = new();
    [CascadingParameter (Name = "FilterView")] StateFilterViewModell? StateFilterViewModell { get; set; }
    [Parameter] public EventCallback<StateFilterViewModell> OnClick { get; set; }

    private async Task CheckedFilterChanged()
    {
        isOpen = !isOpen;
        await OnClick.InvokeAsync(StateFilterViewModell);
    }

    private async Task OpenFilter()
    {
       isOpen = !isOpen;
    }
}

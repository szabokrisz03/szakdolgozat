using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.Forms;

public partial class WiLinkTemplateForm
{
    private MudForm? Form;
    [Parameter] public WiLinkTemplateViewModel Model { get; set; }

}

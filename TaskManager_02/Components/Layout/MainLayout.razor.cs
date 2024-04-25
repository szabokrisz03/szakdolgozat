using Microsoft.AspNetCore.Components.Web;

using MudBlazor;

namespace TaskManager_02.Components.Layout;

public partial class MainLayout
{
    private MudTheme Theme = new()
    {
        Palette = new()
        {
            Primary = "#FBB033",
            Secondary = "#EE281F",
        }
    };
}

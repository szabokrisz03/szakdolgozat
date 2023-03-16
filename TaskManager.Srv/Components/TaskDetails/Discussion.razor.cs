using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Discussion
{
    [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
    [Parameter] public string Comment { get; set; } = "";
    private List<CommentViewModel> _comments = new();
    private string userName = "";

    public async void AddComments()
    {
        var authState = await authenticationStateTask;
        userName = authState.User.Identity?.Name ?? "";
        userName = userName?.Split('\\').Last();

        _comments.Add(new CommentViewModel
        {
            UserName = authState.User.Identity?.Name ?? "",
            Comment = Comment,
            CreationDate = DateTime.Now,
        });
    }
}

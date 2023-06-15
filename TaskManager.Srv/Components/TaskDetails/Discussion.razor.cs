using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.TaskServices.DiscussionServices;

namespace TaskManager.Srv.Components.TaskDetails;

public partial class Discussion
{
    [CascadingParameter (Name = "TaskId")] long Id { get; set; }
    [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
    [Parameter] public string Comment { get; set; } = "";

    [Inject] private ICommentService commentService { get; set; } = null!;

    private string authName = "";
    private List<CommentViewModel> commentViewModels = new();

    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationStateTask;
        authName = authState.User.Identity?.Name ?? "";
        await ListComments();
    }

    public async Task AddComments()
    {
        var authState = await authenticationStateTask;
        authName = authState.User.Identity?.Name ?? "";

        await commentService.CreateCommentAsync(Id, authName, Comment);
        await ListComments();
        Comment = "";
    }

    public async Task DeleteComment(string username, CommentViewModel commentView)
    {
        var authState = await authenticationStateTask;
        authName = authState.User.Identity?.Name ?? "";

        await commentService.DeleteCommentAsync(username, authName, commentView.RowId);
        await ListComments();
    }

    public async Task ListComments()
    {
        commentViewModels = await commentService.ListComments(Id);
    }
}

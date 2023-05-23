using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.TaskServices.DiscussionServices;

public interface ICommentService
{
    Task CreateCommentAsync(long taskId, string userName, string content);
    Task<List<CommentViewModel>> ListComments(long taskId);
    Task DeleteCommentAsync(string userName, string loggedUser, long commentId);
}
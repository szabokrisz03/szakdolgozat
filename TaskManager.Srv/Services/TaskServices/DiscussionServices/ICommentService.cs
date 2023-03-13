using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.TaskServices.DiscussionServices;

public interface ICommentService
{
    Task<CommentViewModel> CreateCommentAsync(CommentViewModel commentViewModel);
    Task<List<CommentViewModel>> ListComments(long taskId);
}

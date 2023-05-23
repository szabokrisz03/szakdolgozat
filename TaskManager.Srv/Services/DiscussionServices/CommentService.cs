using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using System.Threading.Tasks;

using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.UtilityServices;

namespace TaskManager.Srv.Services.TaskServices.DiscussionServices;

public class CommentService : ICommentService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;
    private readonly IUserService userService;

    public CommentService(IMapper mapper, IDbContextFactory<ManagerContext> dbContextFactory, IUserService userService)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
        this.userService = userService;
    }

    public async Task CreateCommentAsync(long taskId, string userName, string content)
    {
        var user = await userService.GetUser(userName);
        long userId = user!.RowId;

        CommentLine commentLine = new()
        {
            TaskId = taskId,
            Comment = content,
            UserId = userId,
        };

        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.CommentLine.AddAsync(commentLine);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(commentLine).State = EntityState.Detached;
        }
    }

    public async Task DeleteCommentAsync(string userName, string loggedUser, long commentId)
    {
        var createdUser = await userService.GetUser(userName);
		long createdUserId = createdUser!.RowId;
        var logged = await userService.GetUser(loggedUser);
		var loggedId = logged!.RowId;

		if (loggedId == createdUserId)
		{
			using (var dbcx = await dbContextFactory.CreateDbContextAsync())
			{
				await dbcx.CommentLine
					.Where(p => p.RowId == commentId)
					.ExecuteDeleteAsync();
			}
		}
    }

    public async Task<List<CommentViewModel>> ListComments(long taskId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var lst = await dbcx.CommentLine
                .AsNoTracking()
                .Where(t => t.TaskId == taskId)
                .OrderByDescending(t => t.CreationDate)
                .Join(dbcx.User.AsNoTracking(), t => t.UserId, u => u.RowId, (comm, user) => new { comm, user })
                .ToListAsync();

            return lst.Select(join =>
            {
                var commentVm = mapper.Map<CommentViewModel>(join.comm);
                commentVm.UserName = join.user.UserName;

                return commentVm;
            }).ToList();
        }
    }
}

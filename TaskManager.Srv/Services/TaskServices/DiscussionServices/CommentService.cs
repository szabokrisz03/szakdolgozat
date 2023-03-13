using AutoMapper;

using Microsoft.EntityFrameworkCore;

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

    public async Task<CommentViewModel> CreateCommentAsync(CommentViewModel commentViewModel)
    {
        var comment = mapper.Map<CommentLine>(commentViewModel);
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.CommentLine.AddAsync(comment);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(comment).State = EntityState.Detached;
        }

        return mapper.Map<CommentViewModel>(comment);
    }

    public async Task<List<CommentViewModel>> ListComments(long taskId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var lst = await dbcx.CommentLine
                .AsNoTracking()
                .Where(t => t.RowId == taskId)
                .OrderBy(t => t.CreationDate)
                .ToListAsync();

            return lst.Select(mapper.Map<CommentViewModel>).ToList();
        }
    }
}

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using TaskManager.Srv.Model.DataContext;

namespace TaskManager.Srv.Services.TaskServices;

public class TaskDisplayService : ITaskDisplayService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;

    public TaskDisplayService(IMapper mapper, IDbContextFactory<ManagerContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<bool> TaskNameExistsAsync(long projectId, string name)
    {
        using(var dbcx = dbContextFactory.CreateDbContext())
        {
            return await dbcx.ProjectTask
                .AsNoTracking()
                .Where(p => p.ProjectId == projectId && p.Name == name)
                .AnyAsync();
        }
    }
}
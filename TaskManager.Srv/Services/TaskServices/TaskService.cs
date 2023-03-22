using AutoMapper;
using Microsoft.EntityFrameworkCore;

using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Services.UtilityServices;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TaskManager.Srv.Services.TaskServices;

public class TaskService : ITaskService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;

    public TaskService(IMapper mapper, IDbContextFactory<ManagerContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<int> CountTasks(long projectId)
    {
        using(var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            return await dbcx.ProjectTask
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .CountAsync();
        }
    }

    public async Task<List<TaskViewModel>> ListTasks(long projectId, int take = 10, int skip = 0)
    {
        using(var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var lst = await dbcx.ProjectTask
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return lst.Select(t => mapper.Map<TaskViewModel>(t)).ToList();
        }
    }

    public async Task UpdateTask(TaskViewModel taskViewModel)
    {
        var task = mapper.Map<ProjectTask>(taskViewModel);

        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var entry = dbcx.Attach(task);
            entry.Property(e => e.State).IsModified = true;
            await dbcx.SaveChangesAsync();

            entry.State = EntityState.Detached;
        }
    }

    public async Task<TaskViewModel> CreateTask(TaskViewModel taskView)
    {
        var task = mapper.Map<ProjectTask>(taskView);
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.ProjectTask.AddAsync(task);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(task).State = EntityState.Detached;
        }

        return mapper.Map<TaskViewModel>(task);
    }
}
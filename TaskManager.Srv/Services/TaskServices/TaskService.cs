using AutoMapper;
using Microsoft.EntityFrameworkCore;

using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Services.UtilityServices;
using System.Runtime.CompilerServices;

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

    public async Task<TaskViewModel> CreateTask(TaskViewModel taskView)
    {
        var task = mapper.Map<ProjectTask>(taskView);
        task.RowId = 0;
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.ProjectTask.AddAsync(task);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(task).State = EntityState.Detached;
        }

        return mapper.Map<TaskViewModel>(task);
    }
}
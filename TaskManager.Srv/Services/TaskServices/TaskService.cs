using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

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

	/// <inheritdoc cref="ITaskService.CountTasks(long)"/>
	public async Task<int> CountTasks(long projectId)
	{
		using (var dbcx = await dbContextFactory.CreateDbContextAsync())
		{
			return await dbcx.ProjectTask
				.AsNoTracking()
				.Where(t => t.ProjectId == projectId)
				.CountAsync();
		}
	}

	/// <inheritdoc cref="ITaskService.ListTasks(long, int, int)"/>
	public async Task<List<TaskViewModel>> ListTasks(long projectId, int take, int skip = 0)
	{
		using (var dbcx = await dbContextFactory.CreateDbContextAsync())
		{
			var lst = await dbcx.ProjectTask
				.AsNoTracking()
				.Where(t => t.ProjectId == projectId)
				.Skip(skip)
				.Take(take)
				.ToListAsync();

			return lst.Select(mapper.Map<TaskViewModel>).ToList();
		}
	}

	public async Task UpdateTaskDb(TaskViewModel modell)
	{
		if(modell.RowId == 0)
		{
			return;
		}

		var ent = mapper.Map<ProjectTask>(modell);

		using (var dbcx = await dbContextFactory.CreateDbContextAsync())
		{
			dbcx.ProjectTask.Update(ent);
			await dbcx.SaveChangesAsync();
		}
	}

	/// <inheritdoc cref="ITaskService.UpdateStatus(TaskViewModel)"/>
	public async Task UpdateStatus(TaskViewModel taskViewModel)
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

	/// <inheritdoc cref="ITaskService.CreateTask(TaskViewModel)"/>
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
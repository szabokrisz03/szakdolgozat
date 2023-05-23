using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.WiServices;

public class WiService : IWiService
{

	private readonly IDbContextFactory<ManagerContext> dbContextFactory;
	private readonly IMapper mapper;

	public WiService(
		IDbContextFactory<ManagerContext> dbContextFactory,
		IMapper mapper)
	{
		this.dbContextFactory = dbContextFactory;
		this.mapper = mapper;
	}

	public async Task<WiViewModel> CreateWiAsync(WiViewModel wiViewModel)
	{
		var wi = mapper.Map<ConnectingWiDb>(wiViewModel);
		using (var dbcx = await dbContextFactory.CreateDbContextAsync())
		{
			await dbcx.ConnectingWiDb.AddAsync(wi);
			await dbcx.SaveChangesAsync();
			dbcx.Entry(wi).State = EntityState.Detached;
		}

		return mapper.Map<WiViewModel>(wi);
	}

	public async Task<List<WiViewModel>> ListWorkItem(long taskId)
	{
		using (var dbcx = await dbContextFactory.CreateDbContextAsync())
		{
			var lst = await dbcx.ConnectingWiDb
				.AsNoTracking()
				.Where(wi => wi.TaskId == taskId)
				.ToListAsync();

			return lst.Select(p => mapper.Map<WiViewModel>(p)).ToList();
		}
	}

}

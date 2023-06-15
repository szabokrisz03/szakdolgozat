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

    public async Task CreateWiAsync(int wiId, long taskId)
    {
        ConnectingWiDb wi = new()
        {
            WiId = wiId,
            TaskId = taskId,
        };

        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.ConnectingWiDb.AddAsync(wi);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(wi).State = EntityState.Detached;
        }
    }

    public async Task<int[]> ListWorkItem(long taskId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var lst = await dbcx.ConnectingWiDb
                .AsNoTracking()
                .Where(wi => wi.TaskId == taskId)
                .ToListAsync();

            return lst.Select(p => p.WiId).ToArray();
        }
    }

}

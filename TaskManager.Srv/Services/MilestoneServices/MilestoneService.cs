using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.MilestoneServices;

public class MilestoneService : IMilestoneService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;

    public MilestoneService(IMapper mapper, IDbContextFactory<ManagerContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<List<MilestoneViewModel>> ListMilestones(long TaskId)
    {

        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var lst = await dbcx.TaskMilestone
                .AsNoTracking()
                .Where(p => p.TaskId == TaskId)
                .ToListAsync();

            return lst.Select(m => mapper.Map<MilestoneViewModel>(m)).ToList();
        }

    }

    public async Task<MilestoneViewModel> CreateMilestone(MilestoneViewModel milestoneView)
    {
        var milestone = mapper.Map<TaskMilestone>(milestoneView);
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.TaskMilestone.AddAsync(milestone);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(milestone).State = EntityState.Detached;
        }

        return mapper.Map<MilestoneViewModel>(milestone);
    }

}

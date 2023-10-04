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

    /// <inheritdoc cref="IMilestoneService.CloseMilestone(long)"/>
    public async Task CloseMilestone(long milestoneId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var result = await dbcx.TaskMilestone
                    .Where(p => p.RowId == milestoneId)
                    .ExecuteUpdateAsync(b => b.SetProperty(u => u.Actual, DateTime.Now));
        }
    }

    public async Task UpdateMilestonekDb(MilestoneViewModel modell)
    {
        if (modell.RowId == 0)
        {
            return;
        }

        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var res = dbcx.TaskMilestone.SingleOrDefault(x => x.RowId == modell.RowId);
            if (res == null)
            {
                return;
            }

            var ent = mapper.Map<MilestoneViewModel, TaskMilestone>(modell, res);
            dbcx.TaskMilestone.Update(ent);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(ent).State = EntityState.Detached;
        }
    }

    /// <inheritdoc cref="IMilestoneService.DeleteMilestone(long)"/>
    public async Task DeleteMilestone(long milestoneId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.TaskMilestone
                .Where(p => p.RowId == milestoneId)
                .ExecuteDeleteAsync();
        }
    }

    /// <inheritdoc cref="IMilestoneService.ListMilestones(long)"/>
    public async Task<List<MilestoneViewModel>> ListMilestones(long TaskId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var lst = await dbcx.TaskMilestone
                .AsNoTracking()
                .Where(p => p.TaskId == TaskId)
                .ToListAsync();

            return lst.Select(mapper.Map<MilestoneViewModel>).ToList();
        }
    }

    /// <inheritdoc cref="IMilestoneService.CreateMilestone(MilestoneViewModel)"/>
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
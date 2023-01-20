using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.WiLinkService;

public class WiLinkTemplateService : IWiLinkTemplateService
{
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;
    private readonly IMapper mapper;

    public WiLinkTemplateService(
        IDbContextFactory<ManagerContext> dbContextFactory,
        IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<int> CountTemplates(long projectId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            return await dbcx.WiLinkTemplate
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .CountAsync();
        }
    }

    public async Task CreateTemplate(WiLinkTemplateViewModel template)
    {
        var ent = mapper.Map<WiLinkTemplate>(template);
        ent.RowId = 0;
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            await dbcx.WiLinkTemplate.AddAsync(ent);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(ent).State = EntityState.Detached;
        }
    }

    public async Task DeleteTemplate(long templateId)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var ent = await dbcx.WiLinkTemplate.Where(l => l.RowId == templateId).SingleOrDefaultAsync();
            if (ent != null)
            {
                dbcx.Entry(ent).State = EntityState.Deleted;
                await dbcx.SaveChangesAsync();
            }
        }
    }

    public async Task<List<WiLinkTemplateViewModel>> ListTemplates(long projectId, int take = 10, int skip = 0)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var lst = await dbcx.WiLinkTemplate
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return lst.Select(t => mapper.Map<WiLinkTemplateViewModel>(t)).ToList();
        }
    }

    public async Task UpdateTemplate(WiLinkTemplateViewModel template)
    {
        if (template.RowId == 0)
        {
            return;
        }

        var ent = mapper.Map<WiLinkTemplate>(template);
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            dbcx.WiLinkTemplate.Update(ent);
            await dbcx.SaveChangesAsync();
        }
    }
}

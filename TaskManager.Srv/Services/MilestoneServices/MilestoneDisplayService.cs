﻿using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using TaskManager.Srv.Model.DataContext;

namespace TaskManager.Srv.Services.MilestoneServices;

public class MilestoneDisplayService : IMilestoneDisplayService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;

    public MilestoneDisplayService(IMapper mapper, IDbContextFactory<ManagerContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<bool> MilestoneNameExistsAsync(long taskId, string name)
    {
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            return await dbcx.TaskMilestone
                .AsNoTracking()
                .Where(p => p.TaskId == taskId && p.Name == name)
                .AnyAsync();
        }
    }
}
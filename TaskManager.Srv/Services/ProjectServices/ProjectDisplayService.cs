using AutoMapper;

using Microsoft.EntityFrameworkCore;
using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.ProjectServices;

public class ProjectDisplayService : IProjectDisplayService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;

    public ProjectDisplayService(
        IMapper mapper,
        IDbContextFactory<ManagerContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<bool> ProjectIdExistsAsync(long rowid)
    {
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            return await dbcx.Project.AsNoTracking().Where(p => p.RowId == rowid).AnyAsync();
        }
    }

    public async Task<bool> ProjectTechnicalNameExistsAsync(Guid technicalName)
    {
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            return await dbcx.Project.AsNoTracking().Where(p => p.TechnicalName == technicalName).AnyAsync();
        }
    }

    public async Task<bool> ProjectNameExistsAsync(string name)
    {
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            return await dbcx.Project.AsNoTracking().Where(p => p.Name == name).AnyAsync();
        }
    }

    public async Task<long> GetProjectIdAsync(Guid technicalName)
    {
        using(var dbcx = dbContextFactory.CreateDbContext())
        {
            var projectId = await dbcx.Project
                .AsNoTracking()
                .Where(p => p.TechnicalName == technicalName)
                .Select(p => p.RowId)
                .SingleOrDefaultAsync();

            return projectId;
        }   
    }

    public async Task<ProjectViewModel?> GetProjectAsync(Guid technicalName)
    {
        Project? project;
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            project = await dbcx.Project
                .AsNoTracking()
                .Where(p => p.TechnicalName == technicalName)
                .SingleOrDefaultAsync();
        }

        return mapper.Map<ProjectViewModel>(project);
    }

    public async Task<ProjectViewModel?> GetProjectAsync(long rowId)
    {
        Project? project;
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            project = await dbcx.Project
                .AsNoTracking()
                .Where(p => p.RowId == rowId)
                .SingleOrDefaultAsync();
        }

        return mapper.Map<ProjectViewModel>(project);
    }

    public async Task<List<ProjectViewModel>> ListProjectsAsync(string searchTerm, int take, int skip = 0)
    {
        List<Project> projects;
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            projects = await dbcx.Project
                .AsNoTracking()
                .Where(p => p.Name.Contains(searchTerm))
                .OrderBy(p => p.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        return projects.Select(mapper.Map<ProjectViewModel>).ToList();
    }

    public async Task<List<ProjectViewModel>> ListUserProjectsAsync(string userName, string searchTerm, int take, int skip = 0)
    {
        List<Project> projects;
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            var p1 = dbcx.User
                .Include(u => u.ProjectUsers)
                .Where(u => u.UserName == userName)
                .ToList();

            projects = await dbcx.User
                .Where(u => u.UserName == userName)
                .SelectMany(u => u.ProjectUsers)
                .Select(pu => pu.Project)
                .Where(p => p.Name.Contains(searchTerm))
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        return projects.Select(p => mapper.Map<ProjectViewModel>(p)).ToList();
    }

    public async Task<List<ProjectViewModel>> ListUserProjectsAsync(string userName, DateTime visitedUntil, int take, int skip = 0)
    {
        List<Project> projects;
        using (var dbcx = dbContextFactory.CreateDbContext())
        {
            projects = await dbcx.User
                .Where(u => u.UserName == userName)
                .SelectMany(u => u.ProjectUsers)
                .Where(pu => pu.LastVisit >= visitedUntil)
                .Select(pu => pu.Project)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        return projects.Select(p => mapper.Map<ProjectViewModel>(p)).ToList();
    }

    public async Task<string?> GetProjectNameAsync(Guid technicalName)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var project = await dbcx.Project.AsNoTracking().Where(p => p.TechnicalName == technicalName).SingleOrDefaultAsync();
            return project?.Name;
        }
    }
}

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using TaskManager_02.Data.Entity;
using TaskManager_02.Data.Context;
using TaskManager_02.Data.ViewModels;
using TaskManager_02.Extensions.Exceptions;

namespace TaskManager_02.Services.ProjectServices;

public class ProjectService : IProjectService
{
    private readonly IDbContextFactory<TaskManagerContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public ProjectService(IDbContextFactory<TaskManagerContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<List<ProjectViewModel>> ListUserProjectsAsync(UserViewModel userViewModel)
    {
        List<Project> projects = new();

        using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
        {
            projects = await dbcx.Projects
                .AsNoTracking()
                .Where(p => p.CreatedBy == userViewModel.RowId)
                .OrderBy(p => p.CreatedOn)
                .ToListAsync();
        }

        return _mapper.Map<List<ProjectViewModel>>(projects);
    }

    public async Task<List<ProjectViewModel>> ListProjectAsync()
    {
        List<Project> projects = new();

        using ( var dbcx = await _dbContextFactory.CreateDbContextAsync() )
        {
            projects = await dbcx.Projects
                .AsNoTracking()
                .OrderBy(p => p.CreatedOn)
                .ToListAsync();
        }

        return _mapper.Map<List<ProjectViewModel>>(projects);
    }

    public async Task<bool> ProjectExistAsync(string projectName)
    {
        using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
        {
            return await dbcx.Projects.Where(x => x.ProjectName == projectName).AnyAsync();
        }
    }

    public async Task<bool> CreateProjectAsync(ProjectViewModel projectViewModel, UserViewModel user)
    {
        try
        {
            if ( await ProjectExistAsync(projectViewModel.ProjectName) )
            {
                throw new TaskManagerException("A projektnév már létezik!");
            }

            if ( projectViewModel.ProjectName.IsNullOrEmpty() )
            {
                throw new TaskManagerException("A projektnév nem lehet üres!");
            } 

            using ( var dbcx = await _dbContextFactory.CreateDbContextAsync() )
            {
                projectViewModel.CreatedBy = user.RowId;
                var Project = _mapper.Map<Project>(projectViewModel);
                await dbcx.Projects.AddAsync(Project);
                await dbcx.SaveChangesAsync();
            }

            return true;
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskManagerException("Hiba történt a létrehozás során!",ex);
        }
    }
}

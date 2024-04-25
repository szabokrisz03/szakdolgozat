using TaskManager_02.Data.ViewModels;

namespace TaskManager_02.Services.ProjectServices;

public interface IProjectService
{
    /// <summary>
    /// 
    ///     Ellenőrzi, hogy az adott projektnév létezik-e már az adatbázisban.
    /// 
    /// </summary>
    /// <param name="projectName"> A felhasználó által megadott projektnév </param>
    /// <returns>
    ///     true -> ha létezik
    ///     false -> ha nem létezik
    /// </returns>
    Task<bool> ProjectExistAsync(string projectName);

    /// <summary>
    /// 
    ///     Létrehozza a projektet az adatbázisban.
    ///     
    /// </summary>
    /// <param name="projectViewModel"> A létrehozandó projekt adatai </param>
    /// <returns>
    ///     true -> ha sikeres a hozzáadás
    ///     false -> Ha nem sikeres a hozzáadás
    /// </returns>
    Task<bool> CreateProjectAsync(ProjectViewModel projectViewModel, UserViewModel user);

    Task<List<ProjectViewModel>> ListUserProjectsAsync(UserViewModel userViewModel);

    Task<List<ProjectViewModel>> ListProjectAsync();
}

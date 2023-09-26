using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.TaskServices;

/// <summary>
/// Projekt létrehozására.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Feladat létrehozása és feltöltése adatbázisba.
    /// </summary>
    /// <param name="taskView">Létrehozandó feladat</param>
    /// <returns>A létrehozott feladat</returns>
    Task<TaskViewModel> CreateTask(TaskViewModel taskView);

    /// <summary>
    /// Megszámolja, hogy a megadott projektID-hez hány darab feladat tartozik. 
    /// </summary>
    /// <param name="projectId">Projekt egyedi azonosítója</param>
    /// <returns>Darabszám</returns>
    Task<int> CountTasks(long projectId);

    /// <summary>
    /// Kilistázza a megadott projektID-hez tartozó feladatokat.
    /// </summary>
    /// <param name="projectId">Projekt egyedi azonosítója</param>
    /// <param name="take">Eltett</param>
    /// <param name="skip">Kihagyott</param>
    /// <returns>Feladatokat tartalmazó lista</returns>
    Task<List<TaskViewModel>> ListTasksById(long projectId, int take, int skip = 0);

    Task<List<TaskViewModel>> ListTasksByFilterAndId(List<string> filterName, long projectId, int take, int skip = 0);

    Task UpdateTaskDb(TaskViewModel modell);

    /// <summary>
    /// Feladat státuszainak a frissítését végzi el.
    /// </summary>
    /// <param name="taskState">A frissítendő feladat</param>
    Task UpdateStatus(TaskViewModel taskState);
}
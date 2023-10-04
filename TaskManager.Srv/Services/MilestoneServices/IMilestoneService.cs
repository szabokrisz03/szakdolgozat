using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.MilestoneServices;

public interface IMilestoneService
{
    /// <summary>
    /// Határidő lezárása teljesítettre. 
    /// </summary>
    /// <param name="milestoneId">Határidő egyedi azonosítója</param>
    Task<List<MilestoneViewModel>> ListMilestones(long TaskId);

    /// <summary>
    /// Határidő törlése.
    /// </summary>
    /// <param name="milestoneId">Határidő egyedi azonosítója</param>
    Task<MilestoneViewModel> CreateMilestone(MilestoneViewModel milestoneView);

    Task UpdateMilestonekDb(MilestoneViewModel modell);

    /// <summary>
    /// Egy feladathoz tartozó határidők kilistázása.
    /// </summary>
    /// <param name="TaskId">Feladat egyedi azonosítója</param>
    /// <returns>Határidőket tartalmazó lista</returns>
    Task CloseMilestone(long milestoneId);

    /// <summary>
    /// Határidő létrehozása és adatbázisba feltöltése.
    /// </summary>
    /// <param name="milestoneView"></param>
    /// <returns>A hozzáadott határidő</returns>
    Task DeleteMilestone(long milestoneId);
}
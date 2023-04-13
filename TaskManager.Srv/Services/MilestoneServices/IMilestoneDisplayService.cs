namespace TaskManager.Srv.Services.MilestoneServices;

public interface IMilestoneDisplayService
{
    Task<bool> MilestoneNameExistsAsync(long taskId, string name);
}

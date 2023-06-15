using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.MilestoneServices;

public interface IMilestoneService
{
    Task<List<MilestoneViewModel>> ListMilestones(long TaskId);
    Task<MilestoneViewModel> CreateMilestone(MilestoneViewModel milestoneView);
    Task CloseMilestone(long milestoneId);
    Task DeleteMilestone(long milestoneId);
}

using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Services.UtilityServices;

public interface IUserService
{
    Task<UserViewModel?> GetUser(string userName);
    Task CreateUser(UserViewModel userModel);
    Task<bool> ExistsUser(string userName);
    Task EnsureUserExists(string userName);
}

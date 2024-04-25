using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TaskManager_02.Data.Entity;
using TaskManager_02.Data.Context;
using TaskManager_02.Data.ViewModels;

namespace TaskManager_02.Services.UserServices;

public class UserService : IUserService
{
    private readonly IDbContextFactory<TaskManagerContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public UserService(IDbContextFactory<TaskManagerContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task CreateUser(UserViewModel userModel)
    {
        if ( await ExistsUser(userModel.UserName))
        {
            return;
        }

        using (var dbcx = await _dbContextFactory.CreateDbContextAsync())
        {
            var user = _mapper.Map<User>(userModel);
            await dbcx.Users.AddAsync(user);
            await dbcx.SaveChangesAsync();
            dbcx.Entry(user).State = EntityState.Detached;
        }
    }

    public async Task EnsureUserExists(string userName)
    {
        if ( await ExistsUser(userName) )
        {
            return;
        }

        var userVm = new UserViewModel()
        {
            UserName = userName,
        };
        await CreateUser(userVm);
    }

    public async Task<bool> ExistsUser(string userName)
    {
        using ( var dbcx = await _dbContextFactory.CreateDbContextAsync() )
        {
            return await dbcx.Users.AsNoTracking().Where(u => u.Username == userName).AnyAsync();
        }
    }

    public async Task<UserViewModel> GetUser(string userName)
    {
        using ( var dbcx = await _dbContextFactory.CreateDbContextAsync() )
        {
            var user = await dbcx.Users.AsNoTracking().Where(u => u.Username == userName).SingleOrDefaultAsync();
            return _mapper.Map<UserViewModel>(user);
        }
    }
}

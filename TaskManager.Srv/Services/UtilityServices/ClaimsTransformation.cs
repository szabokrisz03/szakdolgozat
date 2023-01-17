using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

using TaskManager.Srv.Model.DataContext;
using TaskManager.Srv.Model.DataModel;

namespace TaskManager.Srv.Services.UtilityServices;

public class ClaimsTransformation : IClaimsTransformation
{
    private readonly IDbContextFactory<ManagerContext> dbContextFactory;

    public ClaimsTransformation(
        IDbContextFactory<ManagerContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal is null || principal.Identity is null || !principal.Identity.IsAuthenticated)
        {
            return principal;
        }

        string username = principal.Identity.Name ?? "";
        if (!string.IsNullOrEmpty(username))
        {
            await PersistUser(username);
        }

        return principal;
    }

    private async Task PersistUser(string username)
    {
        using (var dbcx = await dbContextFactory.CreateDbContextAsync())
        {
            var user = await dbcx.User.AsNoTracking().Where(u => u.UserName == username).SingleOrDefaultAsync();
            if (user is null)
            {
                user = new User()
                {
                    UserName = username
                };

                await dbcx.AddAsync(user);
                await dbcx.SaveChangesAsync();
                dbcx.Entry(user).State = EntityState.Detached;
            }
        }
    }
}

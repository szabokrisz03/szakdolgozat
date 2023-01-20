using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

using TaskManager.Srv.Model.DataContext;

namespace TaskManager.Srv.Services.UtilityServices;

public class ClaimsTransformation : IClaimsTransformation
{
    private readonly IUserService userService;

    public ClaimsTransformation(
        IUserService userService)
    {
        this.userService = userService;
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
            await userService.EnsureUserExists(username);
        }

        return principal;
    }
}

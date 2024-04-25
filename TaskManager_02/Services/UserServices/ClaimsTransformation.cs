using Microsoft.AspNetCore.Authentication;

using System.Security.Claims;

namespace TaskManager_02.Services.UserServices;

/// <summary>
/// 
///     Windows authentkációt megvalósító osztály.
/// 
/// </summary>
public class ClaimsTransformation : IClaimsTransformation
{
    private readonly IUserService _userService;

    public ClaimsTransformation(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 
    ///     Windows authentikáció
    ///     Ha a felhasználó még nem lett hozzá adva az adatbázishoz, akkor hozzá adja.
    ///     
    /// </summary>
    /// <returns> A bejelentkezett felhasználó adatai </returns>
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if(principal is null || principal.Identity is null || !principal.Identity.IsAuthenticated)
        {
            return principal!;
        }

        string userName = principal.Identity.Name ?? "";
        if(!string.IsNullOrEmpty(userName))
        {
            await _userService.EnsureUserExists(userName);
        }

        return principal;
    }
}

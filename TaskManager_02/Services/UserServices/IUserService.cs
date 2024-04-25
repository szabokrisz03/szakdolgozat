#nullable enable

using TaskManager_02.Data.ViewModels;

namespace TaskManager_02.Services.UserServices;

public interface IUserService
{
    /// <summary>
    /// 
    ///     Lekéri a felhasználót az adatbázisból.
    /// 
    /// </summary>
    /// <param name="userName"> A felhasználó egyedi neve </param>
    /// <returns> A lekért felhasználó adatai </returns>
    Task<UserViewModel> GetUser(string userName);

    /// <summary>
    /// 
    ///     Létrehozza a felhasználót az adatbázisban.
    /// 
    /// </summary>
    /// <param name="userModel"> A létrehozandó felhasználó adatai </param>
    Task CreateUser(UserViewModel userModel);

    /// <summary>
    /// 
    ///     Ellenőrzi, hogy a felhasználó szerepel-e már az adatbázisban.
    /// 
    /// </summary>
    /// <param name="userName"> A felhasználó egyedi neve </param>
    /// <returns>
    ///     true -> ha a felhasználó már létezik
    ///     false -> ha a felhasználó még nem létezik
    /// </returns>
    Task<bool> ExistsUser(string userName);

    /// <summary>
    /// 
    ///     Ellenőrzi, hogy a felhasználó létezik-e már az adatbázisban.
    ///     Ha létezik -> nem csinál semmit.
    ///     Ha nem létezik -> Létrehozza a felhasználót az adatbázisban.
    /// 
    /// </summary>
    /// <param name="userName"> A felhasználó egyedi neve </param>
    Task EnsureUserExists(string userName);
}

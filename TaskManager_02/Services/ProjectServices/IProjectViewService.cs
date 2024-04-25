namespace TaskManager_02.Services.ProjectServices;

public interface IProjectViewService
{
    /// <summary>
    /// 
    ///     Megnyitja a projekt létehozásához szükséges dialógust.
    /// 
    /// </summary>
    /// <param name="userName"> A felhasználó, aki a projektet hozza létre </param>
    Task OpenProjectDialogAsync(string userName);
}

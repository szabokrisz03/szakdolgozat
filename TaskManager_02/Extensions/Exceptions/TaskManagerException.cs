namespace TaskManager_02.Extensions.Exceptions;

/// <summary>
/// 
///     Saját kivétel osztály
/// 
/// </summary>
[Serializable]
public class TaskManagerException : Exception
{
    public TaskManagerException() { }

    public TaskManagerException(string message) : base(message)
    {
    }

    public TaskManagerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

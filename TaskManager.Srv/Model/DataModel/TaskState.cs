using System;

namespace TaskManager.Srv.Model.DataModel;

[Serializable]
public enum TaskState
{
    ToDo = 0,
    InProgress,
    Done,
}
using System;
using System.Collections.Generic;

namespace TaskManager_02.Data.Entity;

public partial class User
{
    public long RowId { get; set; }
    public string Username { get; set; } = null!;
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}

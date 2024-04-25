using System;
using System.Collections.Generic;

namespace TaskManager_02.Data.Entity;

public partial class Project
{
    public long RowId { get; set; }

    public string ProjectName { get; set; } = null!;

    public Guid TechnicalName { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}

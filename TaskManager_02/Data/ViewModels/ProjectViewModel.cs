#nullable enable

namespace TaskManager_02.Data.ViewModels;

public class ProjectViewModel
{
    public long RowId { get; set; }

    public string ProjectName { get; set; } = null!;

    public Guid TechnicalName { get; set; }

    public long CreatedBy { get; set; }
}

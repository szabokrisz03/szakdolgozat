﻿namespace TaskManager.Srv.Model.ViewModel;

public class MilestoneViewModel
{
    public string Table { get; set; } = "main";
    public long TaskId { get; set; }
    public string Name { get; set; } = "";
    public DateTime? Actual { get; set; }
    public DateTime? Planned { get; set; }
}
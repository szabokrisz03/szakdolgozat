﻿namespace TaskManager.Srv.Model.ViewModel;

public class CommentViewModel
{
    public long TaskId { get; set; }
    public long RowId { get; set; }
    public string UserName { get; set; } = "";
    public string Comment { get; set; } = "";
    public DateTime CreationDate { get; set; }
}
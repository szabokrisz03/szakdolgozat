namespace TaskManager.Srv.Model.DataModel;

public class CommentLine : DbTable
{
    public int TaskId { get; set; }
    public string Comment { get; set; } = "";
    public DateTime CreationDate { get; set; }
}

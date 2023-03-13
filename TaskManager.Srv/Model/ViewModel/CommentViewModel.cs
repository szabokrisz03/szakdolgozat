namespace TaskManager.Srv.Model.ViewModel;

public class CommentViewModel
{
    public int TaskId { get; set; }
    public string Comment { get; set; } = "";
    public DateTime CreationDate { get; set; }
}

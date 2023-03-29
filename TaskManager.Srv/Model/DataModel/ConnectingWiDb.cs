namespace TaskManager.Srv.Model.DataModel;

public class ConnectingWiDb : DbTable
{
    public long WiId { get; set; }
    public long TaskId { get; set; }
    public string? InsertUser { get; set; } = "";
    public DateTime InsertDate { get; set; }
}

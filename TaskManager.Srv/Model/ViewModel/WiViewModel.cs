namespace TaskManager.Srv.Model.ViewModel;

public class WiViewModel
{
	public int? WiId { get; set; }
	public long TaskId { get; set; }
	public string WiName { get; set; } = "";
	public string AssignedTo { get; set; } = "";
	public string WiState { get; set; } = "";
	public string WiDate { get; set; } = "";
}

using Newtonsoft.Json;

namespace TaskManager.Srv.Model.DTO;

[Serializable]
public class WorkItem
{
	[JsonProperty("System.Id")]
	public int Id { get; set; }

	[JsonProperty("System.State")]
	public string State { get; set; } = "";

	[JsonProperty("System.AssignedTo")]
	public AzdoUser? AssignedTo { get; set; }

	[JsonProperty("System.ChangedDate")]
	public DateTime ChangedDate { get; set; }

	[JsonProperty("System.Title")]
	public string Title { get; set; } = "";

	[JsonIgnore]
	public string? AssignedToId => AssignedTo?.UniqueName;

}
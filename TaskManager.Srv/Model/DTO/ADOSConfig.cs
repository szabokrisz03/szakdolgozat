namespace TaskManager.Srv.Model.DTO;

[Serializable]
public class ADOSConfig
{
	public string ApiVer { get; set; } = "5.0";
	public string Host { get; set; } = "azdoapp.griffsoft.hu/tfs";
	public string Collection { get; set; } = "Test";
	public string TeamProject { get; set; } = "t_taskmanager";
}

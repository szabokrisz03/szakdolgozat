using TaskManager.Srv.Model.DTO;

namespace TaskManager.Srv.Utilities;

public class ADOSUrls
{
	private static string GetWiDetailsUrl(ADOSConfig config, IEnumerable<int> wis, IEnumerable<string> fields)
	=> $"https://{config.Host}/{config.Collection}/{config.TeamProject}/_apis/wit/workitems?ids={string.Join(",", wis)}&api-version={config.ApiVer}&fields={string.Join(",", fields)}";

	private static string GetQueryUrl(ADOSConfig config)
	=> $"https://{config.Host}/{config.Collection}/{config.TeamProject}/_apis/wit/wiql?api-version={config.ApiVer}";

	public static string GetDetailUrl(ADOSConfig config, IEnumerable<int> wis, IEnumerable<string> fields)
	{
		return GetWiDetailsUrl(config, wis, fields);
	}

	public static string GetUrl(ADOSConfig config)
	{
		return GetQueryUrl(config);
	}
}

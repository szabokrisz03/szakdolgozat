namespace TaskManager.Srv.Utilities;

public class PreparedHttpRequest
{
	public PreparedHttpRequest(string url)
	{
        Url = url;
    }

    public PreparedHttpRequest(string url, HttpContent content)
    {
        Url = url;
        Content = content;
    }

    public string Url { get; init; }
    public HttpContent? Content { get; init; }
}

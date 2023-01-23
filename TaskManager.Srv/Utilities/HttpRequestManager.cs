namespace TaskManager.Srv.Utilities;

public static class HttpRequestManager
{
    public static async Task<T?> GetAsync<T>(this HttpClient client, PreparedHttpRequest request)
    {
        using (var response = await client.GetAsync(request.Url))
        {
            return await response.Read<T>();
        }
    }
}

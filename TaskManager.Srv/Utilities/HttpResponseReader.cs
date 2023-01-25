using Newtonsoft.Json;

namespace TaskManager.Srv.Utilities;

public static class HttpResponseReader
{
    public static async Task<T?> Read<T>(this HttpResponseMessage msg)
    {
        msg.Validate();

        using (var bodyStream = await msg.Content.ReadAsStreamAsync())
        using (var reader = new StreamReader(bodyStream))
        {
            string json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        using (var bodyStream = await msg.Content.ReadAsStreamAsync())
        using (var reader = new StreamReader(bodyStream))
        using (var jsonReader = new JsonTextReader(reader))
        {
            var ser = JsonSerializer.Create();
            return ser.Deserialize<T>(jsonReader);
        }
    }

    public static void Validate(this HttpResponseMessage msg)
    {
        // TODO
    }
}

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using System.Collections.Immutable;

using TaskManager.Srv.Extensions;
using TaskManager.Srv.Model.Options;
using TaskManager.Srv.Utilities;

namespace TaskManager.Srv.Services.AzdoServices;

public class AzdoDataFetchService: IAzdoDataFetchService
{
    private readonly IOptions<AppSettings> options;
    private readonly IMemoryCache memoryCache;
    private readonly IHttpClientFactory httpClientFactory;

    public AzdoDataFetchService(
        IOptions<AppSettings> options,
        IMemoryCache memoryCache,
        IHttpClientFactory httpClientFactory)
    {
        this.options = options;
        this.memoryCache = memoryCache;
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<ImmutableList<string>> GetTeams()
    {
        var lst =
        await memoryCache.GetOrCreateAsync(CacheKeys.TEAM_PROJECTS, async (entry) =>
        {
            entry.SetAbsoluteExpiration(DateTimeOffset.Now.AddSeconds(5));
            return await GetTeamsFromAzdo()!;
        });

        return lst!;
    }

    private PreparedHttpRequest MakeGetTeamsRequest() => new HttpRequestBuilder("teams")
        .SetQueryParam("api-version", "6.0-preview.3")
        .Build();

    private async Task<ImmutableList<string>> GetTeamsFromAzdo()
    {
        var request = MakeGetTeamsRequest();
        using (var client = httpClientFactory.CreateClient(HttpClients.AZDO_ORG_GET))
        {
            return (await HttpRequestManager.GetAsync<ImmutableList<string>>(client, request))!;
        }
    }
}

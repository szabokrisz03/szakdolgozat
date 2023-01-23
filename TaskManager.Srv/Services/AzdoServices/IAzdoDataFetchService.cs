using System.Collections.Immutable;

namespace TaskManager.Srv.Services.AzdoServices;

public interface IAzdoDataFetchService
{
    Task<ImmutableList<string>> GetTeams();
}

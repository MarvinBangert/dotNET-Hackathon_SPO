using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;

class GraphHelper
{
    // Settings object
    private static Settings? _settings;
    // App-only auth token credential
    private static ClientSecretCredential? _clientSecredCredential;
    // Client configured with app-only authentication
    private static GraphServiceClient? _appClient;

    public static void InitializeGraphForAppOnlyAuth(Settings settings)
    {
        // Ensure settings isn't null
        _ = settings ??
            throw new System.NullReferenceException("Settings cannot be null");

        _settings = settings;

        if (_clientSecredCredential == null)
        {
            _clientSecredCredential = new ClientSecretCredential(
                _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
        }

        if (_appClient == null)
        {
            _appClient = new GraphServiceClient(_clientSecredCredential,
                // Use the default scope, which will request the scopes
                // configured on the app registration
                new[] { "https://graph.microsoft.com/.default" });
        }
    }

    public static async Task<string> GetAppOnlyTokenAsync()
    {
        // Ensure credentia isn't null
        _ = _clientSecredCredential ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

        // Request token with given scopes
        var context = new TokenRequestContext(new[] { "https://graph.microsoft.com/.default" });
        var response = await _clientSecredCredential.GetTokenAsync(context);
        return response.Token;
    }

    public static Task<Microsoft.Graph.Models.SiteCollectionResponse?> GetSitesAsync()
    {
        // Ensure client isn't null
        _ = _appClient ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

        return _appClient
            .Sites
            .GetAsync(requestConfiguration => {
                requestConfiguration.QueryParameters.Select = new string[] { "Id","WebUrl" };
                requestConfiguration.QueryParameters.Top = 5; });
    }

    //public static Task GetSiteActivitiesAsync(string siteId, string today, string earlier)
    //{
    //    // Ensure client isn't null
    //    _ = _appClient ??
    //        throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

    //    var response = _appClient.Sites[siteId]
    //        .GetActivitiesByIntervalWithStartDateTimeWithEndDateTimeWithInterval(earlier, today, "month")
    //        .GetAsync();


    //    //int c = 0;

    //    //var result = response
    //    //    .Result
    //    //    .Value;

    //    //foreach( var item in result )
    //    //{
    //    //    Console.WriteLine(item.Access.ActionCount.ToString());
    //    //    c = c + item.Access.ActionCount ?? 0;
    //    //}

    //    //Console.WriteLine(c);
    //}
}
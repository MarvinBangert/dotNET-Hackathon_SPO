using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

Console.WriteLine("Microsoft Graph & .NET Hackathon 2023\n");

var settings = Settings.LoadSettings();

// Initialize Graph
InitializeGraph(settings);

int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Display access token");
    Console.WriteLine("2. Get all SharePoint sites");

    try
    {
        choice = int.Parse(Console.ReadLine() ?? string.Empty);
    }
    catch (System.FormatException )
    {
        // Set to invalid value
        choice = -1;
    }

    switch(choice) 
    {
        case 0:
            // Exit the program
            Console.WriteLine("Goodby...");
            break;
        case 1:
            // Display access token
            await DisplayAccessTokenAsync();
            break;
        case 2:
            // Get all SharePoint Sites
            await GetSharePointSites();
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }
}

void InitializeGraph(Settings settings)
{
    GraphHelper.InitializeGraphForAppOnlyAuth(settings);
}

async Task DisplayAccessTokenAsync()
{
    try 
    {
        var appOnlyToken = await GraphHelper.GetAppOnlyTokenAsync();
        Console.WriteLine($"App-only token: {appOnlyToken}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting app-only access token: {ex.Message}");
    }
}

async Task GetSharePointSites()
{
    try
    {
        //var today = DateTime.Today.ToString("yyyy-MM-dd");
        //var earlier = DateTime.Today.AddDays(-90).ToString("yyyy-MM-dd");
        
        var siteResult = await GraphHelper.GetSitesAsync();

        if(siteResult != null && siteResult.Value != null)
        {
            Console.WriteLine("WebUrl,siteId");
            foreach (var site in siteResult.Value)
            {
                if(site.Id != null)
                {
                    var siteId = site.Id.Split(',')[1];
                    //Task siteActivity = GraphHelper.GetSiteActivitiesAsync(siteId, today, earlier);
                    Console.WriteLine($"{site.WebUrl} {siteId}");
                }
                else
                {
                    Console.WriteLine("Site ID is empty"); 
                }
            }
        } 
        else 
        { 
            Console.WriteLine("siteResult is empty"); 
        }
    }
    catch (ODataError odataError)
    {
        Console.WriteLine($"Error getting sites: {odataError.Error.Message}");
        throw;
    }
}
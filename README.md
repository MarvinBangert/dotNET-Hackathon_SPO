# Microsoft Graph & .NET Hackathon 2023

[![Hack Together: Microsoft Graph and .NET](https://img.shields.io/badge/Microsoft%20-Hack--Together-orange?style=for-the-badge&logo=microsoft)](https://github.com/microsoft/hack-together)

I used this Hackathon to learn .NET and C#, so really taking my first steps. I really learned a lot! Thanks for that already!

This is a small console app that will return all SharePoint sites and their site id. I actually planned to go more deeper into what to return from SharePoint sites, like owner and last modified date, so I could send them an email (or something). Unfortunately I couldn't find a proper solution so currently it only returns the access token (from the AAD app registration) and all SharePoint sites + their site id in the connected tenant.

![image](https://user-images.githubusercontent.com/50577590/225178070-0b7fb85a-13d7-4740-bbb1-f8185b501a13.png)

## Setup
* Create a new App registration within AAD
* API permissions
  * Sites.Read.All (Application)
* Grant admin consent
* Create a new client secret and copy the value
* Copy client ID and tenant ID from "Overview"

## Inside the script
Before you can start, update "appsettings.json" with the values from your AAD app registration

using Visual Studio or VS Code or command line, switch into the the folder of the app and start it (e.g. using "dotnet run"). You should be able to press 1 to see your token and verify it on https://jwt.ms/ and pressing 2 you should see all your SharePoint site collections and their id.

//1. Import Libraries
using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
namespace az_active_auth
{
    class Program
    {
        //2. Update Main Method
        static async Task Main (string[] args){
            //3. Client ID + Tenant ID
            const string _clientID = "c75fa98e-6c43-4f3f-9768-3abb31a85384";
            const string _tenantID = "9bbb2b0f-1905-4262-b30b-72f1acff58d0";

            //4. Build App
            var app  = PublicClientApplicationBuilder
            .Create(_clientID)
            .WithAuthority(AzureCloudInstance.AzurePublic,_tenantID)
            .WithRedirectUri("Http://localhost:9001")
            .Build();

            //5. Provide Permission to read profile
            string[] scopes = {"user.read"};
            //6. Get the token using Interactive Authentication
            AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
            Console.WriteLine($"Obtained Token :\t {result.AccessToken}");


        }
    }
}

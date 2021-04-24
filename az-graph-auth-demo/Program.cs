using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;



namespace az_graph_auth_demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
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
            //6. Graph Operation Get the token using Interactive Authentication
            var provider = new InteractiveAuthenticationProvider(app,scopes);
            var client = new GraphServiceClient(provider);
            
            // Get the Profile Details
            User me = await client.Me.Request().GetAsync();
            Console.WriteLine($"Display Name  :\t {me.DisplayName}"); 
            // AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
            // Console.WriteLine($"Obtained Token :\t {result.AccessToken}"); 
        }
    }
}

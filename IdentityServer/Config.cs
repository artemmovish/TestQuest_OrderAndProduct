using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { 
                new ApiScope(name: "order", displayName: "Order API")
            };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client
                {
                    ClientId = "Admin",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("orders".Sha256())
                    },

                    AllowedScopes = { "order" }
                }
            };
}

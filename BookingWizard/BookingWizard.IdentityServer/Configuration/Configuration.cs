using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace BookingWizard.IdentityServer.Configuration
{
    public static class Configuration
    {
        
        public static  IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> { 
                new ApiResource("BookingWizard"),
                new ApiResource("BookingWizard.Client")
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256())},

                    AllowedGrantTypes =GrantTypes.ClientCredentials,

                    AllowedScopes={ "BookingWizard" }
                },
                new Client
                {
                    ClientId="client_id_mvc",
                    ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                  //  RedirectUris = {""},

                    AllowedScopes={  "BookingWizard", "BookingWizard.Client" }
                }

            };
    }
}

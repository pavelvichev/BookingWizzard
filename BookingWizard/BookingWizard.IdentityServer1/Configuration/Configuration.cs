using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
namespace BookingWizard.IdentityServer.Configuration
{
    public static class Configuration
    {

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
             new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),

            };
        
        public static IEnumerable<ApiScope> GetApiScopes() =>
    new List<ApiScope> { new ApiScope("BookingWizard", "BookingWizard API"),
                         new ApiScope("BookingWizard.Client" ,"BookingWizard.Client API")};
        public static IEnumerable<ApiResource> GetApiResources() =>
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

                    AllowedGrantTypes =GrantTypes.ClientCredentials,

                    AllowedScopes={ "BookingWizard","BookingWizard.Client", IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile },

                    RedirectUris = { "https://localhost:7081/signin-oidc" }

                   
                },


               

            };
    }
}

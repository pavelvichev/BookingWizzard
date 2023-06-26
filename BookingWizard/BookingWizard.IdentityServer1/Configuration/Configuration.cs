﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace BookingWizard.IdentityServer.Configuration
{
    public static class Configuration
    {

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
             new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "role.scope",
                    UserClaims =
                    {
                        "role"
                    }
                }


            };
        
        public static IEnumerable<ApiScope> GetApiScopes() =>
    new List<ApiScope> { new ApiScope("BookingWizard", "BookingWizard API"),
                         new ApiScope("BookingWizard.Client" ,"BookingWizard.Client API")};
        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> {
                new ApiResource("BookingWizard"),
                new ApiResource("BookingWizard.Client"),
                
                new ApiResource(LocalApi.ScopeName,new[] { JwtClaimTypes.Role })
            };


        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256())},

                    AllowedGrantTypes =GrantTypes.ClientCredentials,

                    AllowedScopes={ StandardScopes.OpenId,
                    StandardScopes.Profile,"BookingWizard", }
                },

                new Client
                {
                    ClientId="client_id_mvc",
                    ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},

                    AllowedGrantTypes =GrantTypes.Code,

                    AllowedScopes={ "BookingWizard","BookingWizard.Client", StandardScopes.OpenId,StandardScopes.Profile, "role.scope" },

                    RedirectUris = { "https://localhost:44328/signin-oidc" },
                    
                    AlwaysIncludeUserClaimsInIdToken = true

                    
                   
                },


               

            };
    }
}
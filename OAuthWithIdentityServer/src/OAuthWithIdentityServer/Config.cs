using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthWithIdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "mvc",
                     ApiSecrets = new List<Secret> { new Secret("superSecretPassword".Sha256() )},
                     Scopes = new List<Scope>
                     {
                         new Scope("open"),
                         new Scope("slack_user_id"),
                         new Scope(IdentityServerConstants.StandardScopes.OpenId),
                         new Scope(IdentityServerConstants.StandardScopes.Profile),
                         new Scope(IdentityServerConstants.StandardScopes.Email)
                     }
                }
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new List<Client>
                {
                    new Client
                    {
                        ClientId = "mvc",
                        ClientName = "MVC Client",
                        AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                        AllowAccessTokensViaBrowser = true,
                        // where to redirect to after login
                        RedirectUris = { "http://localhost:55046/signin-oidc" },
                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "http://localhost:55046/" },
                        AllowedScopes = new List<string>
                        {
                            "open",
                            IdentityServerConstants.StandardScopes.Email,
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "slack_user_id"
                        },
                        //RequireClientSecret = false,
                        ClientSecrets = new List<Secret>
                        {
                            new Secret("superSecretPassword".Sha256())
                        },
                        //AccessTokenType = AccessTokenType.Reference,
                        AllowOfflineAccess = true,
                    }


                    //new Client
                    //{
                    //    ClientId = "lol",
                    //    ClientName = "LOL",
                    //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //    AllowAccessTokensViaBrowser = true,
                    //    // where to redirect to after login
                    //    RedirectUris = { "http://localhost:64154/signin-oidc" },
                    //    // where to redirect to after logout
                    //    PostLogoutRedirectUris = { "http://localhost:64154" },
                    //    AllowedScopes = new List<string>
                    //    {
                    //        "open",
                    //        IdentityServerConstants.StandardScopes.OpenId,
                    //        IdentityServerConstants.StandardScopes.Profile,
                    //    },
                    //    //RequireClientSecret = false,
                    //    ClientSecrets = new List<Secret>
                    //    {
                    //        new Secret("superSecretPassword".Sha256())
                    //    },
                    //    AllowOfflineAccess = true
                    //}
                };
        }

        public IEnumerable<Scope> Scopes()
        {
            return new List<Scope>()
            {
                new Scope(IdentityServerConstants.StandardScopes.Email),
                new Scope(IdentityServerConstants.StandardScopes.Profile),
                new Scope(IdentityServerConstants.StandardScopes.OpenId)
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}

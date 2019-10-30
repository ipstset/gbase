using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace GamebaseApi.Auth
{
    public class AuthConfig
    {
        public static IEnumerable<Client> Clients = new List<Client>
        {
            //PASSWORD
            new Client
            {
                ClientId = "internal_dev",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "gamebase_api",
                    "gamebase_user"
                }
            },

            new Client
            {
                ClientId = "gbnet-app",
                AllowedGrantTypes = GrantTypes.Implicit,
                RequireClientSecret = false,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "gamebase_api",
                    "gamebase_user"
                },
                AllowAccessTokensViaBrowser = true,
                RedirectUris = {
                    "https://gamebase.netlify.com/callback.html",
                    "https://gamebase.netlify.com/popup.html",
                    "https://gamebase.netlify.com/silent.html"
                },
                PostLogoutRedirectUris = { "https://gamebase.netlify.com/index.html" },
                AllowedCorsOrigins = { "https://gamebase.netlify.com" },
                RequireConsent = false,
                AccessTokenLifetime = 3600,
                IdentityTokenLifetime = 300
            },
            
            //TEST JS APP
            new Client
            {
                ClientId = "test_app_2",
                AllowedGrantTypes = GrantTypes.Implicit,
                RequireClientSecret = false,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "gamebase_api",
                    "gamebase_user"
                },
                AllowAccessTokensViaBrowser = true,
                RedirectUris = {
                    "http://localhost:5010/callback.html",
                    "http://localhost:5010/popup.html",
                    "http://localhost:5010/silent.html"
                },
                PostLogoutRedirectUris = { "http://localhost:5010/index.html" },
                AllowedCorsOrigins = { "http://localhost:5010" },
                RequireConsent = false,
                AccessTokenLifetime = 3600,
                IdentityTokenLifetime = 300
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),

            //custom user info
            new IdentityResource(
                name:        "gamebase_user",
                displayName: "Gamebase User Info",
                claimTypes:  new[] { "roles" })
        };

        public static IEnumerable<ApiResource> Apis = new List<ApiResource>
        {
            new ApiResource("gamebase_api", "Gamebase Api")
            {
                //claims we want passed back in the jwt
                //UserClaims = new [] { "email", "roles" } 
                UserClaims = new [] { "roles" }
            }
        };

    }
}

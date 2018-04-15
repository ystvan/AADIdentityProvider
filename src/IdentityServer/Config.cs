using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    /// <summary>
    /// For development, configuration in a separate class
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// In-memory user objects for testing. Not intended for modeling users in production.
        /// </summary>
        /// <returns>A list of TestUsers</returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                //Adding testusers
                new TestUser
                {
                    SubjectId = "70577986-a27e-45d6-88e6-de8325eae09a",
                    Username = "admin",
                    Password = "password",

                    //Initializes a new instance of the System.Security.Claims.Claim class with the
                    //specified claim type and value.
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Kea"),
                        new Claim("family_name", "Smith"),
                        new Claim("email", "kea@gmail.com"),
                        new Claim("email_verified", "true"),
                        new Claim("role", "Admin"),
                    }
                }
                
            };
        }

        /// <summary>
        /// Scopes related to Identity related information
        /// </summary>
        /// <returns>A list of Convenience class that defines standard identity resources</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //Define which claims are returned to the client
            return new List<IdentityResource>
            {
                //openId is required
                new IdentityResources.OpenId(),

                //additional scopes (optional)
                new IdentityResources.Profile(),
                new IdentityResources.Email(),                
                
            };
        }

        /// <summary>
        /// Resource scope: API who can request info from the IdP
        /// </summary>
        /// <returns>A list of Models that are OpenID Connect or OAuth2 API clients</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("API name", "API DisplayName", new List<string>() { "role" })
            };
        }

        /// <summary>
        /// Client applications who can request info from the IdP
        /// </summary>
        /// <returns>A list of Models that are OpenID Connect or OAuth2 clients</returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "KEA Client App",
                    ClientId = "keaclient-sysint-1",
                    AllowedGrantTypes = new[] {GrantType.Hybrid},
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44358/signin-oidc"

                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        
                    },
                    ClientSecrets =
                    {
                        new Secret("@mysupersecret321$".Sha256())
                    },
                    //for development, include claims, no need to call userinfo endpoint explicitly yet
                    //including claims will result much bigger token size!!!
                    AlwaysIncludeUserClaimsInIdToken = true,                    
                    PostLogoutRedirectUris = { "https://localhost:44358/signout-callback-oidc" }
                    
                }
            };

        }
    }
}

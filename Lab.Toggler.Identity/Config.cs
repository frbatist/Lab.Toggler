using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Lab.Toggler.Identity
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Toggler.Api", "Feature toggler api.")
                {
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.Profile(), 
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Xpto.01",                    
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("Xpto012018".Sha256())
                    },
                    AllowedScopes =
                    {
                        "Toggler.Api",
                        StandardScopes.Profile, "roles"
                    }
                },
                new Client
                {
                    ClientId = "Xpto.02",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("Xpto022018".Sha256())
                    },
                    AllowedScopes = { "Toggler.Api", StandardScopes.Profile }
                },
                new Client
                {
                    ClientId = "Xpto.03",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("Xpto032018".Sha256())
                    },
                    AllowedScopes = { "Toggler.Api", StandardScopes.Profile }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "adminToggle2018",                   
                    Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }

    }
}

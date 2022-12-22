using System.Globalization;
using OpenIddict.Abstractions;
using ScrumBoard.Common.Identity;

namespace ScrumBoard.Authentication
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            var introspection = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByClientIdAsync("apitest") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "apitest",
                    ClientSecret = "ab3ed3c9-d467-4dbf-aeac-f765c874b67f",
                    ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                    DisplayName = "test api",
                    PostLogoutRedirectUris = 
                    {
                        new Uri("https://localhost:44338/signout-callback-oidc")
                    },
                        RedirectUris =
                    {
                        new Uri("https://localhost:44338/signin-oidc")
                    },
                        Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "metadata",
                        OpenIddictConstants.Permissions.Prefixes.Scope + "authorization",
                    },
                        Requirements =  
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }
            if (await manager.FindByClientIdAsync("ScrumBoardFrontendApp") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "ScrumBoardFrontendApp",
                    ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                    ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                    DisplayName = "ScrumBoard FrontEnd app",
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:433/signout-callback-oidc")
                    },
                    RedirectUris =
                    {
                        new Uri("https://localhost:433/signin-oidc")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,
                        OpenIddictConstants.Permissions.ResponseTypes.CodeToken,
                        OpenIddictConstants.Permissions.ResponseTypes.CodeIdTokenToken,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "frontend",
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }

                });
            }
            if (await manager.FindByClientIdAsync("Metadata") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "Metadata",
                    ClientSecret = "bf726f87-6c5a-4a90-952e-81906169ef10",
                    ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                    DisplayName = "Metadata API",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }
            if (await manager.FindByClientIdAsync("Authorization") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "Authorization",
                    ClientSecret = "1bd66f3c-80e3-11ed-a1eb-0242ac120002",
                    ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                    DisplayName = "Authorization API",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }

            if (await introspection.FindByNameAsync("metadata") is null)
            {
                await introspection.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "metadata",
                    DisplayNames = { { CultureInfo.InvariantCulture, "Metadata api access" } },
                    Resources =
                    {
                        "authorization"
                    }
                });
            }

            if (await introspection.FindByNameAsync("frontend") is null)
            {
                await introspection.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "frontend",
                    DisplayNames = { { CultureInfo.InvariantCulture, "FrontEnd app access" } },
                    Resources =
                    {
                        "Metadata",
                        "Authorization",
                        "ScrumBoardFrontendApp"
                    }
                });
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

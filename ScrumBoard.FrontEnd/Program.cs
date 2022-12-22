using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ScrumBoard.Common.Extensions.WebBuilderExtensions;

namespace ScrumBoard.FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args)
                .AddLocalAppSettings();
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSpaStaticFiles(config => config.RootPath = "ClientApp/dist");
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
            })
            .AddOpenIdConnect(options =>
            {
                options.ClientId = "ScrumBoardFrontendApp";
                options.ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654";

                options.RequireHttpsMetadata = false;
                options.GetClaimsFromUserInfoEndpoint = false;
                options.SaveTokens = true;

                // Use the authorization code flow.
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

                // Note: setting the Authority allows the OIDC client middleware to automatically
                // retrieve the identity provider's configuration and spare you from setting
                // the different endpoints URIs or the token validation parameters explicitly.
                options.Authority = "https://localhost:5001";

                options.Scope.Add("email");
                options.Scope.Add("roles");
                options.Scope.Add("frontend");

                // Disable the built-in JWT claims mapping feature.
                options.MapInboundClaims = false;

                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSpa(config =>
                {
                    config.UseProxyToSpaDevelopmentServer("https://localhost:3000");
                    config.Options.SourcePath = "/ClientApp";
                });
            }
            else
            {
                app.MapFallbackToFile("index.html");
            }

            app.Run();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ScrumBoard.Common.Extensions
{
    public static class HelperExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirstValue("sub");
        public static async Task<string?> GetUserAccessToken(this HttpContext context) => await context.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
    }
}

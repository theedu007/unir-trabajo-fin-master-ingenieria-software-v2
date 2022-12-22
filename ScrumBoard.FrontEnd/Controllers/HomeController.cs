using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ScrumBoard.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [HttpGet("~/")]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var token = await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectParameterNames.AccessToken);
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("The access token cannot be found in the authentication ticket. " +
                                                    "Make sure that SaveTokens is set to true in the OIDC options.");
            }

            return View("Home");
        }
    }
}

using System.Net.Http.Headers;
using System.Text.Json;
using ScrumBoard.Common.Application.Entities;
using ScrumBoard.Common.Extensions;
using ScrumBoard.Common.HttpClients;

namespace ScrumBoard.FrontEnd.Services
{
    public class WorkspaceService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public WorkspaceService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<WorkspaceUi>> GetWorkspaceForUserAsync(CancellationToken cancellationToken = default)
        {
            if (_httpContextAccessor.HttpContext is null)
                throw new ArgumentException("Error en el contexto http");

            var httpClient = _httpClientFactory.CreateClient(HttpClientNames.Backend);

            if (httpClient is null)
                throw new Exception("No se pudo crear el cliente http");

            var accessToken = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync();
            if (accessToken is null)
                throw new Exception("No se pudo obtener el token del usuario");

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/WorkspaceUi");

            requestMessage.Headers.Clear();
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage, cancellationToken);
            response.EnsureSuccessStatusCode();

            var workspaces =
                await response.Content.ReadFromJsonAsync<List<WorkspaceUi>>(cancellationToken: cancellationToken);

            if (workspaces is null)
                throw new JsonException("No se pudo serializar la respuesta");

            return workspaces;
        }
    }
}

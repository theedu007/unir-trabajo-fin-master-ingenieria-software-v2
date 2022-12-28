using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ScrumBoard.Common.Application;
using ScrumBoard.Common.Application.Entities;
using ScrumBoard.Common.Extensions;
using ScrumBoard.Common.HttpClients;
using ScrumBoard.Common.Identity;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.BackEnd.Services
{
    public class WorkspaceUiService
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public WorkspaceUiService(ApplicationDbContext applicationContext, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<WorkspaceUi>> GetWorkspacesForUserAsync(CancellationToken cancellationToken = default)
        {
            if (_httpContextAccessor.HttpContext is null)
                throw new ArgumentException("Error en el contexto http");

            var httpClient = _httpClientFactory.CreateClient(HttpClientNames.Authorization);

            if (httpClient is null)
                throw new Exception("No se pudo crear el cliente http");

            var accessToken = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync();
            if (accessToken is null)
                throw new Exception("No se pudo obtener el token del usuario");

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/Workspace/");

            requestMessage.Headers.Clear();
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage, cancellationToken);
            response.EnsureSuccessStatusCode();

            var workspaceResponse = await response.Content.ReadFromJsonAsync<List<Workspace>>(cancellationToken: cancellationToken);

            if (workspaceResponse is null)
            {
                throw new JsonException("No se pudo serializar la respuesta");
            }

            var guids = workspaceResponse.Select(x => x.PublicKey).ToList();
            var filter = Builders<WorkspaceUi>.Filter.In(x => x.PublicKey, guids);

            var uiWorkspaces = await _applicationContext.Workspaces
                .FindAsync(filter, cancellationToken: cancellationToken);

            return await uiWorkspaces.ToListAsync(cancellationToken) ?? new List<WorkspaceUi>();
        }

        public async Task<WorkspaceUi> CreateWorkspaceAsync(WorkspaceUi workspaceUi, CancellationToken cancellationToken = default)
        {
            if (_httpContextAccessor.HttpContext is null)
                throw new ArgumentException("Error en el contexto http");

            var httpClient = _httpClientFactory.CreateClient(HttpClientNames.Authorization);

            if (httpClient is null)
                throw new Exception("No se pudo crear el cliente http");

            var accessToken = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync();
            if (accessToken is null)
                throw new Exception("No se pudo obtener el token del usuario");

            var workspace = new Workspace
            {
                DisplayName = workspaceUi.Name,
                PublicKey = workspaceUi.PublicKey
            };

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Workspace/create");

            requestMessage.Headers.Clear();
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(workspace), Encoding.UTF8, "application/json");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage, cancellationToken);
            response.EnsureSuccessStatusCode();

            var workspaceResponse = await response.Content.ReadFromJsonAsync<Workspace>(cancellationToken: cancellationToken);

            if (workspaceResponse is null)
            {
                throw new JsonException("No se pudo serializar la respuesta");
            }

            workspaceUi.PublicKey = workspaceResponse.PublicKey;

            await _applicationContext.Workspaces.InsertOneAsync(workspaceUi, cancellationToken: cancellationToken);
            var entry = _applicationContext.Workspaces.AsQueryable()
                .FirstOrDefault(x => x.PublicKey == workspaceUi.PublicKey);

            return entry ?? throw new Exception("No se pudo crear el espacio de trabajo");
        }
    }
}

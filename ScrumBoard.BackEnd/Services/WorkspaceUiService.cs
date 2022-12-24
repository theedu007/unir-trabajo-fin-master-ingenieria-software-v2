using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ScrumBoard.Common.Application;
using ScrumBoard.Common.Application.Entities;
using ScrumBoard.Common.Identity;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.BackEnd.Services
{
    public class WorkspaceUiService
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkspaceUiService(ApplicationDbContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Workspace>> GetWorkspacesForUserAsync(Guid userGuid, CancellationToken cancellationToken = default)
        {
            return new List<Workspace>();
        }

        public async Task<WorkspaceUi> CreateWorkspaceAsync(WorkspaceUi workspace, CancellationToken cancellationToken = default)
        {
            await _applicationContext.Workspaces.InsertOneAsync(workspace, cancellationToken: cancellationToken);
            var entry = _applicationContext.Workspaces.AsQueryable()
                .FirstOrDefault(x => x.PublicKey == workspace.PublicKey);

            return entry ?? throw new Exception("No se pudo crear el espacio de trabajo");
        }
    }
}

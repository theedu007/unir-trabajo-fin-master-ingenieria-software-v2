using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ScrumBoard.Common.Application;
using ScrumBoard.Common.Application.Entities;
using ScrumBoard.Common.Identity;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.BackEnd.Services
{
    public class WorkspaceService
    {
        private readonly ApplicationDbContext _applicationContext;

        public WorkspaceService(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
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

using System.Security;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ScrumBoard.Common.Identity;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.Authorization.Services
{
    public class WorkspaceService
    {
        private readonly ApplicationIdentityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkspaceService(ApplicationIdentityDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Workspace> CreateWorkspaceForUserAsync(Workspace newWorkspace, CancellationToken cancellationToken = default)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdString))
                throw new ArgumentException("User id is null");

            if (!int.TryParse(userIdString, out var userId))
                throw new ArgumentException("Failed to parse user id");

            var user = _context.Users
                .Include(x => x.UserWorkpaces)
                .FirstOrDefault(x => x.Id == userId);

            if (user == null)
                throw new ArgumentException("User was not found");


            var entry = await _context.AddAsync(newWorkspace, cancellationToken);
            var newUserWorkspace = new UserWorkspace
            {
                AcceptedInvitation = true,
                UserId = user.Id,
                DateOfAcceptedInvitation = DateTime.Now,
                IsOwner = true,
                User = user,
                Workspace = newWorkspace
            };
            user.UserWorkpaces.Add(newUserWorkspace);
            await _context.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}

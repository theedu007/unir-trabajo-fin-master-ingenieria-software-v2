using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumBoard.Authorization.Services;
using ScrumBoard.Common.Dtos;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspaceController : ControllerBase
    {
        private WorkspaceService _workspaceService;

        public WorkspaceController(WorkspaceService workpsaService)
        {
            _workspaceService = workpsaService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkspaceForUserAsync([FromBody] WorkspaceDto workspaceDto, CancellationToken cancellationToken)
        {
            try
            {
                var workspace = new Workspace
                {
                    DisplayName = workspaceDto.DisplayName,
                    PublicKey = Guid.NewGuid(),
                };
                var result = await _workspaceService.CreateWorkspaceForUserAsync(workspace, cancellationToken);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Failed to create a new workspace");
            }
        }
    }
}

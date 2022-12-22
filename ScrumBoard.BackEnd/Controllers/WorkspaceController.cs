using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumBoard.BackEnd.Services;
using ScrumBoard.Common.Application.Entities;
using ScrumBoard.Common.Dtos;

namespace ScrumBoard.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspaceController : ControllerBase
    {
        private readonly WorkspaceService _workspaceService;

        public WorkspaceController(WorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetWokspacesForUserAsync(Guid userGuid)
        {
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkspaceAsync([FromBody] WorkspaceUiDto dto, CancellationToken cancellationToken)
        {
            var entity = new WorkspaceUi
            {
                Name = dto.Name,
                PublicKey = dto.PublicKey
            };

            var newEntity = await _workspaceService.CreateWorkspaceAsync(entity, cancellationToken);

            return Ok(newEntity);
        }
    }
}

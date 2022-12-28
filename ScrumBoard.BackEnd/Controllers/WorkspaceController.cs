using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumBoard.BackEnd.Services;
using ScrumBoard.Common.Application.Entities;
using ScrumBoard.Common.Dtos;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspaceController : ControllerBase
    {
        private readonly WorkspaceUiService _workspaceService;
        private readonly IMapper _mapper;

        public WorkspaceController(WorkspaceUiService workspaceService, IMapper mapper)
        {
            _workspaceService = workspaceService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetWokspacesForUserAsync(Guid userGuid)
        {
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkspaceAsync([FromBody] WorkspaceUiDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<WorkspaceUi>(dto);
                var newEntity = await _workspaceService.CreateWorkspaceAsync(entity, cancellationToken);
                return Ok(_mapper.Map<WorkspaceUiDto>(newEntity));
            }
            catch (Exception e)
            {
                return BadRequest("No se pudo crear el espacio de trabajo");
            }
        }
    }
}

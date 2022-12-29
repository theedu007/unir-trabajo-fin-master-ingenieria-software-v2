using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumBoard.Common.Dtos;
using ScrumBoard.FrontEnd.Services;

namespace ScrumBoard.FrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspaceController : ControllerBase
    {
        private readonly WorkspaceService _workspaceService;
        private readonly IMapper _mapper;

        public WorkspaceController(WorkspaceService workspaceService, IMapper mapper)
        {
            _workspaceService = workspaceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkspacesForUser(CancellationToken cancellationToken)
        {
            var result = await _workspaceService.GetWorkspaceForUserAsync(cancellationToken);
            return Ok(_mapper.Map<IList<WorkspaceUiDto>>(result));
        }
    }
}

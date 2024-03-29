﻿using AutoMapper;
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
        private readonly WorkspaceService _workspaceService;
        private readonly IMapper _mapper;

        public WorkspaceController(WorkspaceService workspaceService, IMapper mapper)
        {
            _workspaceService = workspaceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkspacesForUserAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _workspaceService.GetWorkspaceForUserAsync(cancellationToken);
                return Ok(_mapper.Map<List<WorkspaceDto>>(result));
            }
            catch (Exception)
            {
                return BadRequest("Hubo un error al intentar obtener los espacios de trabajo del usuario");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkspaceForUserAsync([FromBody] WorkspaceDto workspaceDto, CancellationToken cancellationToken)
        {
            try
            {
                var workspace = _mapper.Map<Workspace>(workspaceDto);
                var result = await _workspaceService.CreateWorkspaceForUserAsync(workspace, cancellationToken);
                return Ok(_mapper.Map<WorkspaceDto>(result));
            }
            catch (Exception)
            {
                return BadRequest("Failed to create a new workspace");
            }
        }
    }
}

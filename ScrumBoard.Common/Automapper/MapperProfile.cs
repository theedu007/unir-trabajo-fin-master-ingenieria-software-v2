using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ScrumBoard.Common.Application.Entities;
using ScrumBoard.Common.Dtos;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.Common.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Workspace, WorkspaceDto>()
                .ReverseMap();

            CreateMap<WorkspaceUi, WorkspaceUiDto>()
                .ReverseMap();
        }
    }
}

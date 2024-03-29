﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard.Common.Dtos
{
    public class WorkspaceUiDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid PublicKey { get; set; } = Guid.Empty;
    }
}

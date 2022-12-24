using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.Common.Dtos
{
    public class WorkspaceDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public Guid PublicKey { get; set; } = Guid.Empty;
    }
}

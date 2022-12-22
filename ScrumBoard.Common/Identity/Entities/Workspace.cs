using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard.Common.Identity.Entities
{
    public class Workspace
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public Guid PublicKey { get; set; } = Guid.Empty;
        public IList<UserWorkspace> UserWorkspaces { get; set; } = new List<UserWorkspace>();
    }
}

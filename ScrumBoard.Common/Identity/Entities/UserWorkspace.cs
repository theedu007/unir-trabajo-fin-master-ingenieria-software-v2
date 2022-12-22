using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard.Common.Identity.Entities
{
    public class UserWorkspace
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public ApplicationUser User { get; set; }
        public Workspace Workspace { get; set; }
        public bool AcceptedInvitation { get; set; }
        public bool IsOwner { get; set; }
        public DateTime DateOfAcceptedInvitation { get; set; }
    }
}

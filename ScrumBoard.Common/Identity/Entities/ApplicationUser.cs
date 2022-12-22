using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ScrumBoard.Common.Identity.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public IList<UserWorkspace> UserWorkpaces { get; set; } = new List<UserWorkspace>();
    }
}

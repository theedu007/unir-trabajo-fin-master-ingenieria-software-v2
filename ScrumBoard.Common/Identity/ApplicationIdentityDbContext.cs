using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScrumBoard.Common.Identity.Entities;

namespace ScrumBoard.Common.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public ApplicationIdentityDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

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
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>,int>
    {
        public ApplicationIdentityDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Workspace> Workspaces => Set<Workspace>();
        public DbSet<UserWorkspace> UserWorkspaces => Set<UserWorkspace>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Workspace>()
                .Property(x => x.Id)
                .IsRequired();
            builder
                .Entity<Workspace>()
                .Property(x => x.PublicKey)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder
                .Entity<Workspace>()
                .Property(x => x.DisplayName)
                .IsRequired();
            builder
                .Entity<Workspace>()
                .HasKey(x => x.Id);
            builder
                .Entity<Workspace>()
                .HasMany(x => x.UserWorkspaces)
                .WithOne(x => x.Workspace);

            builder
                .Entity<UserWorkspace>()
                .HasKey(x => new { x.UserId, x.WorkspaceId });
            builder
                .Entity<UserWorkspace>()
                .HasOne(x => x.Workspace)
                .WithMany(x => x.UserWorkspaces)
                .HasForeignKey(x => x.WorkspaceId);
            builder
                .Entity<UserWorkspace>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserWorkpaces)
                .HasForeignKey(x => x.UserId);

            base.OnModelCreating(builder);
        }
    }
}

using Mvc5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc5.Infra
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure the relationship between User and Role
            modelBuilder.Entity<User>()
                .HasRequired(u => u.Role)  // A User must have a Role
                .WithMany(r => r.Users)    // A Role can have multiple Users
                .HasForeignKey(u => u.RoleId);  // Foreign key in User model

            base.OnModelCreating(modelBuilder);
        }
    }
}
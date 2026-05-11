using Domain.Models;
using Domain.Utilities.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace DAL.DbContexApp
{
    public class DbContextApp : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        public DbContextApp(
            DbContextOptions<DbContextApp> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<TaskItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.UserId);

            base.OnModelCreating(builder);
        }

    }
}

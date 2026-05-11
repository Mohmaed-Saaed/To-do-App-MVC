using DAL.DbContexApp;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace To_do_App.Data
{
    public static class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var scoped = scope.ServiceProvider;

            var context = scoped.GetRequiredService<DbContextApp>();
            var userManager = scoped.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scoped.GetRequiredService<RoleManager<IdentityRole>>();

            // Apply pending migrations (optional in development)
            try
            {
                context.Database.Migrate();
            }
            catch
            {
            }

            // Seed categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Personal" },
                    new Category { Name = "Work" },
                    new Category { Name = "Shopping" },
                    new Category { Name = "Others" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Seed Admin role
            const string adminRoleName = "Admin";
            const string userRoleName = "User";
            var adminRoleExists = roleManager.RoleExistsAsync(adminRoleName).GetAwaiter().GetResult();
            if (!adminRoleExists)
            {
                var role = new IdentityRole(adminRoleName);
                roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }
            var userRoleExists = roleManager.RoleExistsAsync(userRoleName).GetAwaiter().GetResult();
            if (!userRoleExists)
            {
                var role = new IdentityRole(userRoleName);
                roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }

            // Seed admin user
            const string adminEmail = "admin@gmail.com";
            const string adminUserName = "Admin";
            const string adminPassword = "Admin@123"; 

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (createUserResult.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, adminRoleName).GetAwaiter().GetResult();
                }
            }
            else
            {
                // Ensure username matches email so login using email as username works
                if (!string.Equals(adminUser.UserName, adminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    adminUser.UserName = adminEmail;
                    userManager.UpdateAsync(adminUser).GetAwaiter().GetResult();
                }

                var inRole = userManager.IsInRoleAsync(adminUser, adminRoleName).GetAwaiter().GetResult();
                if (!inRole)
                {
                    userManager.AddToRoleAsync(adminUser, adminRoleName).GetAwaiter().GetResult();
                }
            }
        }
    }
}

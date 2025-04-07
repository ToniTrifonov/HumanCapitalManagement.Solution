using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(DbContext context)
        {
            var user = new IdentityUser
            {
                UserName = "admin@admin.com",
                NormalizedUserName = "admin@admin.com",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var roles = new List<IdentityRole>()
            {
                new() { Name = "Admin", NormalizedName = "admin" },
                new() { Name = "ProjectManager", NormalizedName = "projectmanager" },
            };

            foreach (var role in roles)
            {
                if (!context.Set<IdentityRole>().Any(r => r.Name == role.Name))
                {
                    context.Set<IdentityRole>().Add(role);
                }
            }

            if (!context.Set<IdentityUser>().Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "Test123!");
                user.PasswordHash = hashed;

                context.Set<IdentityUser>().Add(user);
                context.Set<IdentityUserRole<string>>().Add(new IdentityUserRole<string>()
                {
                    RoleId = roles[0].Id,
                    UserId = user.Id
                });
            }

            context.SaveChanges();
        }

        public static async Task SeedAsync(DbContext context)
        {
            var user = new IdentityUser
            {
                UserName = "admin@admin.com",
                NormalizedUserName = "admin@admin.com",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var roles = new List<IdentityRole>()
            {
                new() { Name = "Admin", NormalizedName = "admin" },
                new() { Name = "ProjectManager", NormalizedName = "projectmanager" },
            };

            foreach (var role in roles)
            {
                if (!await context.Set<IdentityRole>().AnyAsync(r => r.Name == role.Name))
                {
                    await context.Set<IdentityRole>().AddAsync(role);
                }
            }

            if (!await context.Set<IdentityUser>().AnyAsync(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "Test123!");
                user.PasswordHash = hashed;

                await context.Set<IdentityUser>().AddAsync(user);
                await context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>()
                {
                    RoleId = roles[0].Id,
                    UserId = user.Id
                });
            }

            await context.SaveChangesAsync();
        }
    }
}

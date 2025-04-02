using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Web.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(DbContext context)
        {
            var user = new IdentityUser
            {
                UserName = "AdminUser",
                NormalizedUserName = "adminuser",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var role = new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "admin"
            };

            if (!context.Set<IdentityRole>().Any(r => r.Name == role.Name))
            {
                context.Set<IdentityRole>().Add(role);
            }

            if (!context.Set<IdentityUser>().Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "Test123!");
                user.PasswordHash = hashed;

                context.Set<IdentityUser>().Add(user);
                context.Set<IdentityUserRole<string>>().Add(new IdentityUserRole<string>()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }

            context.SaveChanges();
        }

        public static async Task SeedAsync(DbContext context)
        {
            var user = new IdentityUser
            {
                UserName = "AdminUser",
                NormalizedUserName = "adminuser",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var role = new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "admin"
            };

            if (!(await context.Set<IdentityRole>().AnyAsync(r => r.Name == role.Name)))
            {
                await context.Set<IdentityRole>().AddAsync(role);
            }

            if (!(await context.Set<IdentityUser>().AnyAsync(u => u.UserName == user.UserName)))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "Test123!");
                user.PasswordHash = hashed;

                await context.Set<IdentityUser>().AddAsync(user);
                await context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }

            await context.SaveChangesAsync();
        }
    }
}

using HumanCapitalManagement.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Data
{
    public class DatabaseContext : ApplicationDbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public async Task AddUser(IdentityUser user, string roleId)
        {
            await this.Set<IdentityUser>().AddAsync(user);
            await this.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        public async Task<string> RoleIdByName(string roleName)
        {
            return await this.Set<IdentityRole>()
                .Where(role => role.Name == roleName)
                .Select(role => role.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public async Task<bool> UserEmailInUse(string email)
        {
            return await this.Set<IdentityUser>().AnyAsync(user => user.Email == email);
        }
    }
}

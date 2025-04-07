using HumanCapitalManagement.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddUser(IdentityUser user, string roleId)
        {
            await this.context.Set<IdentityUser>().AddAsync(user);
            await this.context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        public async Task<string> RoleIdByName(string roleName)
        {
            return await this.context.Set<IdentityRole>()
                .Where(role => role.Name == roleName)
                .Select(role => role.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public async Task<bool> UserEmailInUse(string email)
        {
            return await this.context.Set<IdentityUser>().AnyAsync(user => user.Email == email);
        }
    }
}

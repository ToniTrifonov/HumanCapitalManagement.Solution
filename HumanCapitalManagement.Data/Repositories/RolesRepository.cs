using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<IdentityRole> roles;

        public RolesRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.roles = context.Roles;
        }

        public async Task Add(IdentityRole entity)
        {
            await this.roles.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<string> RoleIdByName(string roleName)
        {
            return await this.roles
                .Where(role => role.Name == roleName)
                .Select(role => role.Id)
                .FirstOrDefaultAsync();
        }
    }
}

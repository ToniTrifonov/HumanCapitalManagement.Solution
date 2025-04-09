using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<IdentityUserRole<string>> userRoles;

        public AccountRoleRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.userRoles = context.UserRoles;
        }

        public async Task Add(IdentityUserRole<string> entity)
        {
            await this.userRoles.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }
    }
}

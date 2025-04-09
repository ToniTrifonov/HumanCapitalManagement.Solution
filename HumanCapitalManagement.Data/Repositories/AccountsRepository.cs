using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<IdentityUser> users;

        public AccountsRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.users = context.Users;
        }

        public async Task Add(IdentityUser entity)
        {
            await this.users.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> UserEmailInUse(string email)
        {
            return await this.users.AnyAsync(user => user.Email == email);
        }
    }
}

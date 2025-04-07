using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Data.Contracts
{
    public interface IDatabaseContext
    {
        Task<bool> UserEmailInUse(string email);

        Task<string> RoleIdByName(string roleName);

        Task AddUser(IdentityUser user, string roleId);

        Task<int> SaveChangesAsync();
    }
}

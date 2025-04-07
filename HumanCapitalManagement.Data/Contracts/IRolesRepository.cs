using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Data.Contracts
{
    public interface IRolesRepository : IApplicationRepository<IdentityRole>
    {
        Task<string> RoleIdByName(string roleName);
    }
}

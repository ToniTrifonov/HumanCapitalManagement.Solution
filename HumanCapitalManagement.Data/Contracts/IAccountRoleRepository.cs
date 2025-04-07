using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Data.Contracts
{
    public interface IAccountRoleRepository : IApplicationRepository<IdentityUserRole<string>>
    {
    }
}

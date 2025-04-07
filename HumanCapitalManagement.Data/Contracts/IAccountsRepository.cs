using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Data.Contracts
{
    public interface IAccountsRepository : IApplicationRepository<IdentityUser>
    {
        Task<bool> UserEmailInUse(string email);
    }
}

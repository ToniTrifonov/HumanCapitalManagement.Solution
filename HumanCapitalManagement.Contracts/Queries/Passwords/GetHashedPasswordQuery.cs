using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Contracts.Queries.Passwords
{
    public class GetHashedPasswordQuery
    {
        public GetHashedPasswordQuery(IdentityUser user, string password)
        {
            User = user;
            Password = password;
        }

        public IdentityUser User { get; }

        public string Password { get; }
    }
}

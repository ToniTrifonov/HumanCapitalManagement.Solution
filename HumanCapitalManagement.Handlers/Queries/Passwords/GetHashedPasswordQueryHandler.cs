using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Passwords;
using HumanCapitalManagement.Contracts.Results.Passwords;
using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Handlers.Queries.Passwords
{
    public class GetHashedPasswordQueryHandler : IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult>
    {
        public Task<GetHashedPasswordResult> HandleAsync(GetHashedPasswordQuery query)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();

            var hashedPassword = passwordHasher.HashPassword(query.User, query.Password);
            return Task.FromResult(new GetHashedPasswordResult(hashedPassword));
        }
    }
}

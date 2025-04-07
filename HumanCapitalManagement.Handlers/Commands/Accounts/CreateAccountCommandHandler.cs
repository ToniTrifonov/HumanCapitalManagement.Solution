using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Queries.Passwords;
using HumanCapitalManagement.Contracts.Results.Accounts;
using HumanCapitalManagement.Contracts.Results.Passwords;
using HumanCapitalManagement.Data.Contracts;
using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Handlers.Commands.Accounts
{
    public class CreateAccountCommandHandler : IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>
    {
        private readonly IApplicationRepository repository;
        private readonly IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult> passwordHasher;

        public CreateAccountCommandHandler(
            IApplicationRepository repository,
            IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult> passwordHasher)
        {
            this.repository = repository;
            this.passwordHasher = passwordHasher;
        }

        public async Task<CreateAccountResult> HandleAsync(CreateAccountCommand command)
        {
            var emailInUse = await this.repository.UserEmailInUse(command.Email);
            if (emailInUse)
            {
                return new CreateAccountResult("Email already in use.", succeed: false);
            }

            var roleId = await this.repository.RoleIdByName(command.Role);
            if (roleId == null)
            {
                return new CreateAccountResult("Role does not exist.", succeed: false);
            }

            var newAccount = new IdentityUser()
            {
                UserName = command.Email,
                NormalizedUserName = command.Email,
                Email = command.Email,
                NormalizedEmail = command.Email,
                LockoutEnabled = true,
                EmailConfirmed = true
            };

            var getHashedPassQuery = new GetHashedPasswordQuery(newAccount, command.Password);
            var getHashedPasswordResult = await this.passwordHasher.HandleAsync(getHashedPassQuery);
            newAccount.PasswordHash = getHashedPasswordResult.HashedPassword;

            await this.repository.AddUser(newAccount, roleId);
            await this.repository.SaveChangesAsync();

            return new CreateAccountResult("Account successfully created.", succeed: true);
        }
    }
}

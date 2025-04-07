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
        private readonly IDatabaseContext context;
        private readonly IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult> passwordHasher;

        public CreateAccountCommandHandler(
            IDatabaseContext context,
            IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult> passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<CreateAccountResult> HandleAsync(CreateAccountCommand command)
        {
            var emailInUse = await this.context.UserEmailInUse(command.Email);
            if (emailInUse)
            {
                return new CreateAccountResult("Email already in use.", succeed: false);
            }

            var roleId = await this.context.RoleIdByName(command.Role);
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

            await this.context.AddUser(newAccount, roleId);
            await this.context.SaveChangesAsync();

            return new CreateAccountResult("Account successfully created.", succeed: true);
        }
    }
}

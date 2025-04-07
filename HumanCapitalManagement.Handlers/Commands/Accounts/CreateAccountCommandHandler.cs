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
        private readonly IAccountsRepository accountsRepository;
        private readonly IRolesRepository rolesRepository;
        private readonly IAccountRoleRepository accountRoleRepository;
        private readonly IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult> passwordHasher;

        public CreateAccountCommandHandler(
            IAccountsRepository accountsRepository,
            IRolesRepository rolesRepository,
            IAccountRoleRepository accountRoleRepository,
            IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult> passwordHasher)
        {
            this.accountsRepository = accountsRepository;
            this.rolesRepository = rolesRepository;
            this.accountRoleRepository = accountRoleRepository;
            this.passwordHasher = passwordHasher;
        }

        public async Task<CreateAccountResult> HandleAsync(CreateAccountCommand command)
        {
            var emailInUse = await this.accountsRepository.UserEmailInUse(command.Email);
            if (emailInUse)
            {
                return new CreateAccountResult("Email already in use.", succeed: false);
            }

            var roleId = await this.rolesRepository.RoleIdByName(command.Role);
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

            var newAccountRole = new IdentityUserRole<string>()
            {
                RoleId = roleId,
                UserId = newAccount.Id
            };

            var getHashedPassQuery = new GetHashedPasswordQuery(newAccount, command.Password);
            var getHashedPasswordResult = await this.passwordHasher.HandleAsync(getHashedPassQuery);
            newAccount.PasswordHash = getHashedPasswordResult.HashedPassword;

            await this.accountsRepository.Add(newAccount);
            await this.accountRoleRepository.Add(newAccountRole);

            return new CreateAccountResult("Account successfully created.", succeed: true);
        }
    }
}

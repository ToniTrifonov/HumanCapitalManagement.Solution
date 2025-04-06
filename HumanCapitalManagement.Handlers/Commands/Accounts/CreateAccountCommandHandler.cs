using HumanCapitalManagement.Admin.Data;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Results.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Commands.Accounts
{
    public class CreateAccountCommandHandler : IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>
    {
        private readonly ApplicationDbContext context;

        public CreateAccountCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<CreateAccountResult> HandleAsync(CreateAccountCommand command)
        {
            var emailInUse = await this.context.Set<IdentityUser>().AnyAsync(user => user.Email == command.Email);
            if (emailInUse)
            {
                return new CreateAccountResult("Email already in use.", succeed: false);
            }

            var roleId = await this.context.Set<IdentityRole>()
                .Where(x => x.Name == command.Role)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
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

            var passwordHasher = new PasswordHasher<IdentityUser>();
            var hashedPassword = passwordHasher.HashPassword(newAccount, command.Password);
            newAccount.PasswordHash = hashedPassword;

            await this.context.Set<IdentityUser>().AddAsync(newAccount);
            await this.context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>()
            {
                RoleId = roleId,
                UserId = newAccount.Id
            });

            await this.context.SaveChangesAsync();

            return new CreateAccountResult("Account successfully created.", succeed: true);
        }
    }
}

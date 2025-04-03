namespace HumanCapitalManagement.Contracts.Commands.Accounts
{
    public class CreateAccountCommand
    {
        public CreateAccountCommand(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public string Email { get; }

        public string Password { get; }

        public string Role { get; }
    }
}

using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Results.Accounts;

namespace HumanCapitalManagement.Handlers.Commands.Accounts
{
    public class CreateAccountErrorHandler : IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>
    {
        private readonly IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult> decorated;

        public CreateAccountErrorHandler(IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult> decorated)
        {
            this.decorated = decorated;
        }

        public async Task<CreateAccountResult> HandleAsync(CreateAccountCommand command)
        {
            try
            {
                return await this.decorated.HandleAsync(command);
            }
            catch (Exception)
            {
                return new CreateAccountResult("An unexpected error occurred.", succeed: false);
            }
        }
    }
}

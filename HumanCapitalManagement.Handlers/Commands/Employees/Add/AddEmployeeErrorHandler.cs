using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Add
{
    public class AddEmployeeErrorHandler : IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>
    {
        private readonly IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult> decorated;

        public AddEmployeeErrorHandler(IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult> decorated)
        {
            this.decorated = decorated;
        }

        public async Task<AddEmployeeResult> HandleAsync(AddEmployeeCommand command)
        {
            try
            {
                return await this.decorated.HandleAsync(command);
            }
            catch (Exception)
            {
                return new AddEmployeeResult("An unexpected error occurred.");
            }
        }
    }
}

using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Delete
{
    public class DeleteEmployeeErrorHandler : IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
    {
        private readonly IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult> decorated;

        public DeleteEmployeeErrorHandler(IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult> decorated)
        {
            this.decorated = decorated;
        }

        public async Task<DeleteEmployeeResult> HandleAsync(DeleteEmployeeCommand command)
        {
            try
            {
                return await this.decorated.HandleAsync(command);
            }
            catch (Exception)
            {
                return new DeleteEmployeeResult("An unexpected error occurred.");
            }
        }
    }
}

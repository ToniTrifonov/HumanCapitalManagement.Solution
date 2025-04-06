using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Edit
{
    public class EditEmployeeErrorHandler : IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>
    {
        private readonly IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult> decorated;

        public EditEmployeeErrorHandler(IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult> decorated)
        {
            this.decorated = decorated;
        }

        public async Task<EditEmployeeResult> HandleAsync(EditEmployeeCommand command)
        {
            try
            {
                return await this.decorated.HandleAsync(command);
            }
            catch (Exception)
            {
                return new EditEmployeeResult("An unexpected error occurred.");
            }
        }
    }
}

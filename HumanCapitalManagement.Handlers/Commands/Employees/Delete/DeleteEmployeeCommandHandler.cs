using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Contracts;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Delete
{
    public class DeleteEmployeeCommandHandler : IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
    {
        private readonly IEmployeesRepository repository;

        public DeleteEmployeeCommandHandler(IEmployeesRepository repository)
        {
            this.repository = repository;
        }
        public async Task<DeleteEmployeeResult> HandleAsync(DeleteEmployeeCommand command)
        {
            var employee = await this.repository.EmployeeById(command.Id);
            if (employee == null)
            {
                return new DeleteEmployeeResult("An employee with that id does not exist.");
            }

            await this.repository.DeleteEmployee(command.Id);
            return new DeleteEmployeeResult();
        }
    }
}

using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Contracts;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Edit
{
    public class EditEmployeeCommandHandler : IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>
    {
        private readonly IEmployeesRepository repository;

        public EditEmployeeCommandHandler(IEmployeesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<EditEmployeeResult> HandleAsync(EditEmployeeCommand command)
        {
            var employee = await this.repository.EmployeeById(command.Id);
            if (employee == null)
            {
                return new EditEmployeeResult("Employee does not exist.");
            }

            await this.repository.EditEmployee(command.Id, command.FirstName, command.LastName, command.Salary, command.Position);
            return new EditEmployeeResult();
        }
    }
}

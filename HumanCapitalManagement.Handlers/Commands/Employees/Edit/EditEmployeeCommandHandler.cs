using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Contracts;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Edit
{
    public class EditEmployeeCommandHandler : IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>
    {
        private readonly IApplicationRepository repository;

        public EditEmployeeCommandHandler(IApplicationRepository repository)
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

            employee.FirstName = command.FirstName;
            employee.LastName = command.LastName;
            employee.Salary = command.Salary;
            employee.Position = command.Position;

            await this.repository.SaveChangesAsync();
            return new EditEmployeeResult();
        }
    }
}

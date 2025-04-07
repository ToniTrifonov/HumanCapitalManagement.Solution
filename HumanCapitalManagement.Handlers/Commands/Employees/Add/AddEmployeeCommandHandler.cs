using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Add
{
    public class AddEmployeeCommandHandler : IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>
    {
        private readonly IApplicationRepository repository;

        public AddEmployeeCommandHandler(IApplicationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AddEmployeeResult> HandleAsync(AddEmployeeCommand command)
        {
            var projectExists = await this.repository.ProjectExists(command.ProjectId);
            if (!projectExists)
            {
                return new AddEmployeeResult("Invalid project.");
            }

            var newEmployee = new Employee()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Salary = command.Salary,
                Position = command.Position,
                ProjectId = command.ProjectId,
            };

            await this.repository.AddEmployee(newEmployee);
            await this.repository.SaveChangesAsync();

            return new AddEmployeeResult();
        }
    }
}

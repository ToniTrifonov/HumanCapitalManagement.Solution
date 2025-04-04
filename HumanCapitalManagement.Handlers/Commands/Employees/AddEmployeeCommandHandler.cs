using HumanCapitalManagement.Admin.Data;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Commands.Employees
{
    public class AddEmployeeCommandHandler : IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>
    {
        private readonly ApplicationDbContext context;

        public AddEmployeeCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AddEmployeeResult> HandleAsync(AddEmployeeCommand command)
        {
            var projectExists = await this.context.Set<Project>().AnyAsync(project => project.Id == command.ProjectId);
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
            await this.context.Set<Employee>().AddAsync(newEmployee);
            await this.context.SaveChangesAsync();

            return new AddEmployeeResult("Successfully added new employee.");
        }
    }
}

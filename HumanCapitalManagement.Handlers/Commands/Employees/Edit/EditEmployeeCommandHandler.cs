using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Commands.Employees.Edit
{
    public class EditEmployeeCommandHandler : IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>
    {
        private readonly ApplicationDbContext context;

        public EditEmployeeCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<EditEmployeeResult> HandleAsync(EditEmployeeCommand command)
        {
            var employee = await context.Set<Employee>().FirstOrDefaultAsync(employee => employee.Id == command.Id);
            if (employee == null)
            {
                return new EditEmployeeResult("Employee does not exist.");
            }

            employee.FirstName = command.FirstName;
            employee.LastName = command.LastName;
            employee.Salary = command.Salary;
            employee.Position = command.Position;

            await context.SaveChangesAsync();
            return new EditEmployeeResult();
        }
    }
}

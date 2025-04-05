using HumanCapitalManagement.Admin.Data;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Queries.Employees
{
    public class EmployeeByIdQueryHandler : IAsyncQueryHandler<EmployeeByIdQuery, EmployeeByIdResult>
    {
        private readonly ApplicationDbContext context;

        public EmployeeByIdQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeByIdResult> HandleAsync(EmployeeByIdQuery query)
        {
            var employee = await this.context.Set<Employee>()
                .Where(employee => employee.Id == query.Id)
                .Select(employee => new EmployeeResultItem(employee.Id, employee.FirstName, employee.LastName, employee.Position, employee.Salary))
                .FirstOrDefaultAsync();

            return new EmployeeByIdResult(employee);
        }
    }
}

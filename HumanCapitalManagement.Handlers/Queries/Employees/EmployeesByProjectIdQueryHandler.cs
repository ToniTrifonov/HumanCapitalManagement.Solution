using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Queries.Employees
{
    public class EmployeesByProjectIdQueryHandler : IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult>
    {
        private readonly ApplicationDbContext context;

        public EmployeesByProjectIdQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<EmployeesByProjectIdResult> HandleAsync(EmployeesByProjectIdQuery query)
        {
            var employees = await this.context.Set<Employee>()
                .Where(employee => employee.ProjectId == query.ProjectId)
                .Where(employee => !employee.IsDeleted)
                .Select(employee => new EmployeeResultItem(employee.Id, employee.FirstName, employee.LastName, employee.Position, employee.Salary))
                .ToListAsync();

            return new EmployeesByProjectIdResult(employees);
        }
    }
}

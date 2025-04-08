using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Contracts;

namespace HumanCapitalManagement.Handlers.Queries.Employees
{
    public class EmployeesByProjectIdQueryHandler : IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult>
    {
        private readonly IEmployeesRepository employeesRepository;

        public EmployeesByProjectIdQueryHandler(IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        public async Task<EmployeesByProjectIdResult> HandleAsync(EmployeesByProjectIdQuery query)
        {
            var employees = await this.employeesRepository.EmployeesByProjectId(query.ProjectId);
            var employeesResult = employees
                .Select(employee => new EmployeeResultItem(employee.Id, employee.FirstName, employee.LastName, employee.Position, employee.Salary))
                .ToList();

            return new EmployeesByProjectIdResult(employeesResult);
        }
    }
}

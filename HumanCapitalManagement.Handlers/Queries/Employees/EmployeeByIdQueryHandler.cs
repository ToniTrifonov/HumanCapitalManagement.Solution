using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Contracts;

namespace HumanCapitalManagement.Handlers.Queries.Employees
{
    public class EmployeeByIdQueryHandler : IAsyncQueryHandler<EmployeeByIdQuery, EmployeeByIdResult>
    {
        private readonly IApplicationRepository repository;

        public EmployeeByIdQueryHandler(IApplicationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<EmployeeByIdResult> HandleAsync(EmployeeByIdQuery query)
        {
            var employee = await this.repository.EmployeeById(query.Id);
            var employeeResult = new EmployeeResultItem(employee.Id, employee.FirstName, employee.LastName, employee.Position, employee.Salary);

            return new EmployeeByIdResult(employeeResult);
        }
    }
}

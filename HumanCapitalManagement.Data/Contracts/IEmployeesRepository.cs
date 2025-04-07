using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Enums;

namespace HumanCapitalManagement.Data.Contracts
{
    public interface IEmployeesRepository : IApplicationRepository<Employee>
    {
        Task<Employee> EmployeeById(string employeeId);

        Task DeleteEmployee(string employeeId);

        Task EditEmployee(string id, string firstName, string lastName, decimal salary, EmployeePosition position);
    }
}

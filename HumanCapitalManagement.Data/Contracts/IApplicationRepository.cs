using HumanCapitalManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace HumanCapitalManagement.Data.Contracts
{
    public interface IApplicationRepository
    {
        Task<bool> UserEmailInUse(string email);

        Task<bool> ProjectExists(string projectId);

        Task<string> RoleIdByName(string roleName);

        Task AddUser(IdentityUser user, string roleId);

        Task AddEmployee(Employee employee);

        Task<Employee> EmployeeById(string employeeId);

        Task AddProject(Project project);

        Task<List<Project>> ProjectsByUserId(string userId);

        Task<int> SaveChangesAsync();
    }
}

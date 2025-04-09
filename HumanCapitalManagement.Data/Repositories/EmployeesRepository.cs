using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Employee> employees;

        public EmployeesRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.employees = context.Employees;
        }

        public async Task Add(Employee entity)
        {
            await this.employees.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<Employee> EmployeeById(string employeeId)
        {
            return await this.employees.FirstOrDefaultAsync(employee => employee.Id == employeeId && !employee.IsDeleted);
        }

        public async Task<List<Employee>> EmployeesByProjectId(string projectId)
        {
            return await this.employees
                .Where(employee => employee.ProjectId == projectId)
                .Where(employee => !employee.IsDeleted)
                .ToListAsync();
        }

        public async Task DeleteEmployee(string employeeId)
        {
            var employee = this.employees.Find(employeeId);
            employee.IsDeleted = true;

            await this.context.SaveChangesAsync();
        }

        public async Task EditEmployee(string id, string firstName, string lastName, decimal salary, EmployeePosition position)
        {
            var employee = this.employees.Find(id);

            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Salary = salary;
            employee.Position = position;

            await this.context.SaveChangesAsync();
        }
    }
}

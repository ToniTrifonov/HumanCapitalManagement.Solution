using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddUser(IdentityUser user, string roleId)
        {
            await this.context.Set<IdentityUser>().AddAsync(user);
            await this.context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        public async Task AddEmployee(Employee employee)
        {
            await this.context.Set<Employee>()
                .AddAsync(employee);
        }

        public async Task<bool> ProjectExists(string projectId)
        {
            return await this.context.Set<Project>()
                .AnyAsync(project => project.Id == projectId);
        }

        public async Task<string> RoleIdByName(string roleName)
        {
            return await this.context.Set<IdentityRole>()
                .Where(role => role.Name == roleName)
                .Select(role => role.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public async Task<bool> UserEmailInUse(string email)
        {
            return await this.context.Set<IdentityUser>()
                .AnyAsync(user => user.Email == email);
        }

        public async Task<Employee> EmployeeById(string employeeId)
        {
            return await this.context.Set<Employee>()
                .FirstOrDefaultAsync(employee => employee.Id == employeeId && !employee.IsDeleted);
        }

        public async Task AddProject(Project project)
        {
            await this.context.Set<Project>().AddAsync(project);
        }

        public async Task<List<Project>> ProjectsByUserId(string userId)
        {
            return await this.context.Set<Project>()
                .Where(project => project.UserId == userId)
                .ToListAsync();
        }
    }
}

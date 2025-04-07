using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Data.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Project> projects;

        public ProjectsRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.projects = context.Set<Project>();
        }

        public async Task Add(Project entity)
        {
            await this.projects.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ProjectExists(string projectId)
        {
            return await this.projects.AnyAsync(project => project.Id == projectId);
        }

        public async Task<List<Project>> ProjectsByUserId(string userId)
        {
            return await this.projects
                .Where(project => project.UserId == userId)
                .ToListAsync();
        }
    }
}

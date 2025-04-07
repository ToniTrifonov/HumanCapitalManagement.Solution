using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Queries.Projects
{
    public class ProjectsByUserIdQueryHandler : IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult>
    {
        private readonly ApplicationDbContext context;

        public ProjectsByUserIdQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ProjectsByUserIdResult> HandleAsync(ProjectsByUserIdQuery query)
        {
            var projects = await this.context.Set<Project>()
                .Where(project => project.UserId == query.UserId)
                .Select(project => new ProjectsResultItem()
                {
                    Id = project.Id,
                    CreateDate = project.CreateDate,
                    Size = project.Size,
                    Description = project.Description,
                    Name = project.Name,
                })
                .ToListAsync();

            return new ProjectsByUserIdResult(projects);
        }
    }
}

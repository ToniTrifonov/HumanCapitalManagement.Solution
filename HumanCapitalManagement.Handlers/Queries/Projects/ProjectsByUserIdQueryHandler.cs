using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Data.Contracts;

namespace HumanCapitalManagement.Handlers.Queries.Projects
{
    public class ProjectsByUserIdQueryHandler : IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult>
    {
        private readonly IProjectsRepository repository;

        public ProjectsByUserIdQueryHandler(IProjectsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProjectsByUserIdResult> HandleAsync(ProjectsByUserIdQuery query)
        {
            var projects = await this.repository.ProjectsByUserId(query.UserId);
            var projectsResult = projects.Select(project => new ProjectsResultItem()
            {
                Id = project.Id,
                Size = project.Size,
                CreateDate = project.CreateDate,
                Description = project.Description,
                Name = project.Name,
            }).ToList();

            return new ProjectsByUserIdResult(projectsResult);
        }
    }
}

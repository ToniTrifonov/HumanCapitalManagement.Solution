using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;

namespace HumanCapitalManagement.Handlers.Commands.Projects
{
    public class CreateProjectCommandHandler : IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>
    {
        private readonly IProjectsRepository repository;

        public CreateProjectCommandHandler(IProjectsRepository repository)
        {
            this.repository = repository;
        }
        public async Task<CreateProjectResult> HandleAsync(CreateProjectCommand command)
        {
            var newProject = new Project()
            {
                Name = command.Name,
                Description = command.Description,
                Size = command.Size,
                UserId = command.UserId,
                CreateDate = DateTime.UtcNow,
            };

            await this.repository.Add(newProject);
            return new CreateProjectResult();
        }
    }
}

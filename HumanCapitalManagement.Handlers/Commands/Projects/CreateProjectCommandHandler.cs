using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;

namespace HumanCapitalManagement.Handlers.Commands.Projects
{
    public class CreateProjectCommandHandler : IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>
    {
        private readonly ApplicationDbContext context;

        public CreateProjectCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
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

            await context.Set<Project>().AddAsync(newProject);
            await context.SaveChangesAsync();

            return new CreateProjectResult();
        }
    }
}

using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;

namespace HumanCapitalManagement.Handlers.Commands.Projects
{
    public class CreateProjectErrorHandler : IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>
    {
        private readonly IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult> decorated;

        public CreateProjectErrorHandler(IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult> decorated)
        {
            this.decorated = decorated;
        }

        public async Task<CreateProjectResult> HandleAsync(CreateProjectCommand command)
        {
            try
            {
                return await this.decorated.HandleAsync(command);
            }
            catch (Exception)
            {
                return new CreateProjectResult("An unexpected error occurred.");
            }
        }
    }
}

using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Projects;
using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Handlers.Commands.Projects;
using HumanCapitalManagement.Handlers.Queries.Projects;

namespace HumanCapitalManagement.Web.Extensions
{
    public static class ProjectsServicesExtensions
    {
        public static IServiceCollection RegisterProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult>, ProjectsByUserIdQueryHandler>();

            services.AddScoped<CreateProjectCommandHandler>();
            services.AddScoped<IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>>(sp =>
            {
                var mainHandler = sp.GetRequiredService<CreateProjectCommandHandler>();
                return new CreateProjectErrorHandler(mainHandler);
            });

            return services;
        }
    }
}

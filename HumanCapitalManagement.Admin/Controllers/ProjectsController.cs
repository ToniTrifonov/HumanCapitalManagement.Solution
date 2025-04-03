using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HumanCapitalManagement.Web.Models.Projects;

namespace HumanCapitalManagement.Web.Controllers
{
    [Authorize(Roles = "ProjectManager")]
    public class ProjectsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsHandler;

        public ProjectsController(
            UserManager<IdentityUser> userManager,
            IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsHandler)
        {
            this.userManager = userManager;
            this.projectsHandler = projectsHandler;
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.userManager.GetUserId(User);
            var projectsQuery = new ProjectsByUserIdQuery(userId);
            var projectsResult = await this.projectsHandler.HandleAsync(projectsQuery);

            var viewModel = new AllProjectsViewModel(projectsResult.Projects);

            return View(viewModel);
        }
    }
}

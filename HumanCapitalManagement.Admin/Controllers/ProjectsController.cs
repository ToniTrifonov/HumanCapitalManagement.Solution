using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HumanCapitalManagement.Web.Models.Projects;
using HumanCapitalManagement.Contracts.Commands.Projects;

namespace HumanCapitalManagement.Web.Controllers
{
    [Authorize(Roles = "ProjectManager")]
    public class ProjectsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsHandler;
        private readonly IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult> createProjectHandler;

        public ProjectsController(
            UserManager<IdentityUser> userManager,
            IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsHandler,
            IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult> createProjectHandler)
        {
            this.userManager = userManager;
            this.projectsHandler = projectsHandler;
            this.createProjectHandler = createProjectHandler;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var userId = this.userManager.GetUserId(User);
            var projectsQuery = new ProjectsByUserIdQuery(userId);
            var projectsResult = await this.projectsHandler.HandleAsync(projectsQuery);

            var viewModel = new AllProjectsViewModel(projectsResult.Projects);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var userId = this.userManager.GetUserId(User);

            var createProjectCommand = new CreateProjectCommand(
                inputModel.Name, inputModel.Description, inputModel.Size, userId);
            await this.createProjectHandler.HandleAsync(createProjectCommand);

            return RedirectToAction(nameof(All));
        }
    }
}

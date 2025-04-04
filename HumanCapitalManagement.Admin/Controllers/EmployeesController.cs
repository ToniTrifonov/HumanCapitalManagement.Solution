using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Web.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HumanCapitalManagement.Web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult> employeesByProjectHandler;
        private readonly IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsByUserIdHandler;

        public EmployeesController(
            UserManager<IdentityUser> userManager,
            IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult> employeesByProjectHandler,
            IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsByUserIdHandler)
        {
            this.userManager = userManager;
            this.employeesByProjectHandler = employeesByProjectHandler;
            this.projectsByUserIdHandler = projectsByUserIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> All(string projectId)
        {
            var employeesQuery = new EmployeesByProjectIdQuery(projectId);
            var employeesResult = await this.employeesByProjectHandler.HandleAsync(employeesQuery);

            var viewModel = new AllEmployeesViewModel(employeesResult.Employees);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
    }
}

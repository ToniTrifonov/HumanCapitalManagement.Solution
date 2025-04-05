using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Web.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using HumanCapitalManagement.Contracts.Commands.Employees;

namespace HumanCapitalManagement.Web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult> employeesByProjectHandler;
        private readonly IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsByUserIdHandler;
        private readonly IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult> addEmployeeHandler;

        public EmployeesController(
            UserManager<IdentityUser> userManager,
            IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult> employeesByProjectHandler,
            IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult> projectsByUserIdHandler,
            IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult> addEmployeeHandler)
        {
            this.userManager = userManager;
            this.employeesByProjectHandler = employeesByProjectHandler;
            this.projectsByUserIdHandler = projectsByUserIdHandler;
            this.addEmployeeHandler = addEmployeeHandler;
        }

        [HttpGet]
        public async Task<IActionResult> All(string projectId)
        {
            var employeesQuery = new EmployeesByProjectIdQuery(projectId);
            var employeesResult = await this.employeesByProjectHandler.HandleAsync(employeesQuery);

            var viewModel = new AllEmployeesViewModel(employeesResult.Employees, projectId);

            return View("AllEmployees", viewModel);
        }

        [HttpGet]
        public IActionResult Add(string projectId)
        {
            ViewBag.ProjectId = projectId;
            return View("AddEmployee");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddEmployee", inputModel);
            }

            var addEmployeeCommand = new AddEmployeeCommand(inputModel.FirstName, inputModel.LastName, inputModel.Salary, inputModel.Position, inputModel.ProjectId);
            var addEmployeeResult = await this.addEmployeeHandler.HandleAsync(addEmployeeCommand);

            ViewBag.Message = addEmployeeResult.Message;
            return View("AddEmployee");
        }
    }
}

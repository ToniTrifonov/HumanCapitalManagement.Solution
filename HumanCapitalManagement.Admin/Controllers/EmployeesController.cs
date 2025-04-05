using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Web.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.Web.Controllers
{
    [Authorize(Roles = "ProjectManager")]
    public class EmployeesController : Controller
    {
        private readonly IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult> employeesByProjectHandler;
        private readonly IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult> addEmployeeHandler;

        public EmployeesController(
            IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult> employeesByProjectHandler,
            IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult> addEmployeeHandler)
        {
            this.employeesByProjectHandler = employeesByProjectHandler;
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
        public IActionResult Add()
        {
            return PartialView("_AddEmployee");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .FirstOrDefault();

                return Json(new { ErrorMessage = errorMessage, Succeed = false });
            }

            var addEmployeeCommand = new AddEmployeeCommand(inputModel.FirstName, inputModel.LastName, inputModel.Salary, inputModel.Position, inputModel.ProjectId);
            var addEmployeeResult = await this.addEmployeeHandler.HandleAsync(addEmployeeCommand);

            return Json(new { addEmployeeResult.Succeed, addEmployeeResult.ErrorMessage });
        }
    }
}

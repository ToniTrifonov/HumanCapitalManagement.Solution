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
        private readonly IAsyncQueryHandler<EmployeeByIdQuery, EmployeeByIdResult> employeeByIdHandler;
        private readonly IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult> editEmployeeHandler;

        public EmployeesController(
            IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult> employeesByProjectHandler,
            IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult> addEmployeeHandler,
            IAsyncQueryHandler<EmployeeByIdQuery, EmployeeByIdResult> employeeByIdHandler,
            IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult> editEmployeeHandler)
        {
            this.employeesByProjectHandler = employeesByProjectHandler;
            this.addEmployeeHandler = addEmployeeHandler;
            this.employeeByIdHandler = employeeByIdHandler;
            this.editEmployeeHandler = editEmployeeHandler;
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var employeeByIdQuery = new EmployeeByIdQuery(id);
            var employeeByIdResult = await this.employeeByIdHandler.HandleAsync(employeeByIdQuery);

            var employee = employeeByIdResult.Employee;

            var editEmployeeModel = new EditEmployeeModel()
            {
                Id = id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                Position = employee.Position,
            };

            return PartialView("_EditEmployee", editEmployeeModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .FirstOrDefault();

                return Json(new { ErrorMessage = errorMessage, Succeed = false });
            }

            var editEmployeeCommand = new EditEmployeeCommand(inputModel.Id, inputModel.FirstName, inputModel.LastName, inputModel.Position, inputModel.Salary);
            var editEmployeeResult = await this.editEmployeeHandler.HandleAsync(editEmployeeCommand);

            return Json(new { editEmployeeResult.Succeed, editEmployeeResult.ErrorMessage });
        }
    }
}

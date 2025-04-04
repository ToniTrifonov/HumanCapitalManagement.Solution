using HumanCapitalManagement.Web.Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.Web.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeInputModel inputModel)
        {
            return View();
        }
    }
}

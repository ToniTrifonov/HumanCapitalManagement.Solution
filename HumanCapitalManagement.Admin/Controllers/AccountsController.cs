using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Roles;
using HumanCapitalManagement.Contracts.Results.Roles;
using HumanCapitalManagement.Web.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanCapitalManagement.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        private readonly IAsyncQueryHandler<AllRolesQuery, AllRolesResult> allRolesHandler;

        public AccountsController(IAsyncQueryHandler<AllRolesQuery, AllRolesResult> allRolesHandler)
        {
            this.allRolesHandler = allRolesHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var rolesQuery = new AllRolesQuery();
            var rolesResult = await this.allRolesHandler.HandleAsync(rolesQuery);

            ViewData["Roles"] = new SelectList(rolesResult.Roles, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountModel input)
        {
            return View();
        }
    }
}

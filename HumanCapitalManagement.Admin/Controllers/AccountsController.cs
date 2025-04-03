using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Results.Accounts;
using HumanCapitalManagement.Web.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        private readonly IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult> createAccountHandler;

        public AccountsController(IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult> createAccountHandler)
        {
            this.createAccountHandler = createAccountHandler;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var createAccountCommand = new CreateAccountCommand(input.Email, input.Password, input.Role);
            var createAccountResult = await this.createAccountHandler.HandleAsync(createAccountCommand);
            ViewData["Message"] = createAccountResult.Message;

            return View(input);
        }
    }
}

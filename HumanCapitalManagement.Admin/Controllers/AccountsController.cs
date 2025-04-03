using HumanCapitalManagement.Web.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.Web.Controllers
{
    public class AccountsController : Controller
    {
        public AccountsController()
        {
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountModel input)
        {
            return View();
        }
    }
}

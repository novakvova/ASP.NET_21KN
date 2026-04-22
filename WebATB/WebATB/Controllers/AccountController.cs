using Microsoft.AspNetCore.Mvc;
using WebATB.Models.Account;

namespace WebATB.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet] //Реєстрація нового користувача
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] //Реєстрація нового користувача
        public IActionResult Register(RegisterViewModel model)
        {
            return View();
        }
    }
}

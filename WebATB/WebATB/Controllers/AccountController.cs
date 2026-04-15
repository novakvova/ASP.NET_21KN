using Microsoft.AspNetCore.Mvc;

namespace WebATB.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet] //Реєстрація нового користувача
        public IActionResult Register()
        {
            return View();
        }
    }
}

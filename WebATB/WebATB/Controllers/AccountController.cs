using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebATB.Data;
using WebATB.Data.Entities;
using WebATB.Data.Entities.Identity;
using WebATB.Models.Account;

namespace WebATB.Controllers
{
    public class AccountController(
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager
        ) : Controller
    {
        [HttpGet] //Реєстрація нового користувача
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] //Реєстрація нового користувача
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) //Зберігаємо категорію в БД, якщо модель валідна
            {
                string fileName = "default.jpg";
                //Як зберегти фото
                if (model.FileImage != null)
                {
                    var dir = Directory.GetCurrentDirectory();
                    var wwwroot = "wwwroot";
                    fileName = Guid.NewGuid().ToString()+".jpg";
                    var savePath = Path.Combine(dir, wwwroot, "images", fileName);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        model.FileImage.CopyTo(stream);
                    }
                }
                //Заповнюю таблицю коритувачів в БД
                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    Image = fileName
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index","Categories");
                }
                else
                {
                    ModelState.AddModelError("", "Помилка при реєстрації користувача");
                    return View(model);
                }
            }
            return View(model); // Якщо модель не валідна, повертаємо її назад на форму для виправлення помилок
        }
    }
}

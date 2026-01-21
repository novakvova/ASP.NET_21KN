using Microsoft.AspNetCore.Mvc;
using WebATB.Models.Users;

namespace WebATB.Controllers;

public class MainController : Controller
{
    public IActionResult Index()
    {
        List<UserItemModel> model = new List<UserItemModel>();

        model.Add(new UserItemModel
        {
            Id = 1,
            Name = "Підкаблучник Марко Йосипович",
            Phone = "+38(098)235 6421"
        });
        model.Add(new UserItemModel
        {
            Id = 2,
            Name = "Рижий Олексій Мохнович",
            Phone = "+38(098)230 6421"
        });
        model.Add(new UserItemModel
        {
            Id = 3,
            Name = "Галоша Іванна Ігорівна",
            Phone = "+38(098)236 6421"
        });

        return View(model);
    }
}

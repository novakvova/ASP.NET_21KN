using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Xml.Linq;
using WebATB.Models.Users;
using static System.Net.Mime.MediaTypeNames;

namespace WebATB.Controllers;

public class MainController : Controller
{
    static List<UserItemModel> list =
        new()
        {
            new ()
            {
                Id = 1,
                Name = "Підкаблучник Марко Йосипович",
                Phone = "+38(098)235 6421",
                Image = "1.jpg"
            },
            new ()
            {
                Id = 2,
                Name = "Рижий Олексій Мохнович",
                Phone = "+38(098)230 6421",
                Image = "2.webp"
            },
            new ()
            {
                Id = 3,
                Name = "Галоша Іванна Ігорівна",
                Phone = "+38(098)236 6421",
                Image = "3.jpg"
            }
        };
    public IActionResult Index()
    {
        return View(list);
    }

    [HttpGet] //метод для відображення сторінки створення нового користувача
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(UserCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        //якщо модель валідна, то дані буде зберігати у список
        //і переходимо на іншу сторінку
        UserItemModel item = new UserItemModel
        {
            Id = list.Count + 1,
            Name = model.LastName + " "
                + model.Name + " " + model.MiddleName,
            Phone = model.Phone,
            //Image = model.ImageUrl
        };

        if (model.ImageUrl != null)
        {
            var dir = Directory.GetCurrentDirectory();
            var wwwroot = "wwwroot";
            var fileName = Guid.NewGuid().ToString()+".jpg";
            var savePath = Path.Combine(dir, wwwroot, "images", fileName);
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                model.ImageUrl.CopyTo(stream);
            }
            item.Image=fileName;
        }
        list.Add(item);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        //Шукаю елемента в списку по id
        var item = list.SingleOrDefault(x => x.Id == id);
        return View(item); 
    }

    [HttpPost]
    public IActionResult Delete(UserItemModel user)
    {
        //Шукаю елемента в списку по id
        var item = list.SingleOrDefault(x => x.Id == user.Id);
        list.Remove(item);
        return RedirectToAction(nameof(Index));
    }
}

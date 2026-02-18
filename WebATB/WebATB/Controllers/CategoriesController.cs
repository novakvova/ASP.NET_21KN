using Microsoft.AspNetCore.Mvc;
using WebATB.Data;

namespace WebATB.Controllers;
// робимо Injection для роботи з БД,
// але поки що просто виводимо сторінку
public class CategoriesController(MyContextATB myContextATB) 
    : Controller
{
    public IActionResult Index()
    {
        var categories = myContextATB.Categories.ToList();
        return View(categories);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}

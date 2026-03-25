using Microsoft.AspNetCore.Mvc;

namespace WebATB.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
}

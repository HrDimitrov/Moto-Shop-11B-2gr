using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Yamaha 11B",
                Price = 1500,
                Description = "Mno'o vurvi",
                Category = "Scooter",
                ImageUrl = "/images/yamaha11b.png",
            },
            new Product
            {
                Id = 2,
                Name = "KTM 11B",
                Price = 1600,
                Description = "Vurvi na ku4e",
                Category = "Mega Scooter",
                ImageUrl = "/images/ktm11b.png",
            },
            new Product
            {
                Id = 3,
                Name = "VMX 11B",
                Price = 1600,
                Description = "Tarkalq na ku4e",
                Category = "Mega Scooter",
                ImageUrl = "/images/vmx11b.png",
            },
            new Product
            {
                Id = 3,
                Name = "O'Neal 11B SRS Slick",
                Price = 1600,
                Description = "Mega Zdrava",
                Category = "Mega Scooter",
                ImageUrl = "/images/helmet.png",
            }
        };

        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

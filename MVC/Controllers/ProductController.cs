using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            // Here you would typically fetch the product details from a database
            // For demonstration, we'll create a dummy product
            var product = new Models.Product
            {
                Id = 3,
                Name = "O'Neal 11B SRS Slick",
                Price = 1600,
                Description = "Mega Zdrava",
                Category = "Mega Scooter",
                ImageUrl = "/images/helmet.png",
            };
            return View(product);
        }
    }
}

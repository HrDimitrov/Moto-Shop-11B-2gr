using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Services;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly Db _context;

        public ProductController(Db context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var referer = Request.Headers["Referer"].ToString(); 
            ViewBag.Referer = string.IsNullOrEmpty(referer) ? Url.Action("Index", "Home") : referer;

            var product = _context.Products
        .Include(p => p.Images)
        .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}

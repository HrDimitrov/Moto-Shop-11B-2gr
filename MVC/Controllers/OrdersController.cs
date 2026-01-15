using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC.Services;

namespace MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Db _context;

        public OrdersController(Db context)
        {
            _context = context;
        }

        public IActionResult Index(string email)
        {
            var orders = _context.Orders
                .Where(o => o.Email == email)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .First(o => o.Id == id);

            return View(order);
        }
    }
}

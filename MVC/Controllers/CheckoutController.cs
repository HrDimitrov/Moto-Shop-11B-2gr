using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC.Helpers;
using MVC.Models;
using MVC.Services;

namespace MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Db _context;
        private const string CartKey = "Cart";

        public CheckoutController(Db context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartKey);

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(CheckoutViewModel model)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartKey);

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            // 2️⃣ Създаваме Order
            var order = new Order
            {
                OrderDate = DateTime.Now,
                Status = "Pending",
                CustomerName = model.CustomerName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                TotalAmount = cart.Sum(c => c.Product.Price * c.Quantity),
                OrderItems = new List<OrderItem>()
            };

            // 3️⃣ OrderItems + stock
            foreach (var item in cart)
            {
                var product = _context.Products.First(p => p.Id == item.Product.Id);

                if (product.Quantity < item.Quantity)
                    return BadRequest("Not enough stock");

                product.Quantity -= item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            // 4️⃣ Clear cart
            HttpContext.Session.Remove(CartKey);

            return RedirectToAction("Success", new { id = order.Id });
        }

        public IActionResult Success(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .First(o => o.Id == id);

            return View(order);
        }
    }
}

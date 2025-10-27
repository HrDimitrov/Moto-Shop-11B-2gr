using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Helpers;
public class CartController : Controller
{
    // За опростяване използваме сесия. В реален проект – база данни.
    private const string SessionKey = "Cart";

    private List<CartItem> GetCart()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey);
        if (cart == null)
        {
            cart = new List<CartItem>();
            HttpContext.Session.SetObjectAsJson(SessionKey, cart);
        }
        return cart;
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    [HttpPost]
    public IActionResult Add(int ProductId, int quantity = 1)
    {
        var cart = GetCart();
        // В реален проект вземаш Product от база
        var Product = new Product
        {
            Id = ProductId,
            Name = "R1",
            Price = 35000,
            ImageUrl = "/images/yamaha_r1.jpg"
        };

        var existing = cart.FirstOrDefault(c => c.Product.Id == ProductId);
        if (existing != null)
            existing.Quantity += quantity;
        else
            cart.Add(new CartItem { Product = Product, Quantity = quantity });

        HttpContext.Session.SetObjectAsJson(SessionKey, cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int ProductId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.Product.Id == ProductId);
        if (item != null)
            cart.Remove(item);

        HttpContext.Session.SetObjectAsJson(SessionKey, cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Update(int ProductId, int quantity)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.Product.Id == ProductId);
        if (item != null)
            item.Quantity = quantity;

        HttpContext.Session.SetObjectAsJson(SessionKey, cart);
        return RedirectToAction("Index");
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Helpers;
using MVC.Models;
using MVC.Services;
public class CartController : Controller
{
    private readonly Db _context;
    private const string SessionKey = "Cart";

    public CartController(Db context)
    {
        _context = context;
    }
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
    public IActionResult Add(int productId, int quantity = 1)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);

        if (product == null)
            return NotFound();

        if (product.Quantity < quantity)
        {
            TempData["Error"] = "Not enough stock!";
            return RedirectToAction("Details", "Product", new { id = productId });
        }

        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey)
                   ?? new List<CartItem>();

        var existing = cart.FirstOrDefault(c => c.Product.Id == productId);

        if (existing != null)
        {
            if (existing.Quantity + quantity > product.Quantity)
            {
                TempData["Error"] = "Not enough stock!";
                return RedirectToAction("Details", "Product", new { id = productId });
            }

            existing.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItem
            {
                Product = product,
                Quantity = quantity
            });
        }

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

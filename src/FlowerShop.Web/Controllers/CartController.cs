using FlowerShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Controllers;

/// <summary>Количка. В Етап 1 е само скелет - логиката се добавя в Етап 2 и 3.</summary>
[Authorize]
public class CartController : Controller
{
    public IActionResult Index()
    {
        // TODO (Етап 2): зареждане на CartItems на текущия потребител чрез ICartService.
        return View(new List<CartItemViewModel>());
    }
}

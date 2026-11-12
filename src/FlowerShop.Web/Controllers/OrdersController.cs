using FlowerShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Controllers;

/// <summary>Поръчки на текущия потребител. В Етап 1 е само скелет.</summary>
[Authorize]
public class OrdersController : Controller
{
    public IActionResult Index()
    {
        // TODO (Етап 3): зареждане на поръчките на потребителя чрез IOrderService.
        return View(new List<OrderListItemViewModel>());
    }
}

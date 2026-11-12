using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Controllers;

/// <summary>Собствени страници за грешки: 404 (не е намерено) и 500 (обща грешка).</summary>
[AllowAnonymous]
public class ErrorController : Controller
{
    [Route("Error/{statusCode:int}")]
    public IActionResult Index(int statusCode)
    {
        return statusCode == 404 ? View("404") : View("500");
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Session03.presentationLayer.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}

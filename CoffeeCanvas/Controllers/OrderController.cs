using Microsoft.AspNetCore.Mvc;


namespace CoffeeCanvas.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View("PaymentSuccessful");
        }
    }
}

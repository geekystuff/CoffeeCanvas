using CoffeeCanvas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffeeCanvas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user_Id = HttpContext.Session.GetString("userId");
            if (user_Id != null)
            {
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }

        //public IActionResult Privacy()
        //{
            
        //    return View();
            
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
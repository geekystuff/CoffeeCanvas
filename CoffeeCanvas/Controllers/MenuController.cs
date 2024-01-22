using CoffeeCanvas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeCanvas.Controllers
{
    public class MenuController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationDbContext _context;

        public MenuController(ILogger<HomeController> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var user_Id = HttpContext.Session.GetString("userId");
            var itemCount = _context.Carts
                .Where(x => x.UserId.ToString() == user_Id)
                .Sum(x => x.Quantity);
            ViewBag.ItemCount = itemCount;
            var categories = _context.CoffeeCategories.ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "CoffeeName");
            return View();
        }



        [HttpPost]
        public IActionResult Index(int categoryId)
        {
            var productsInCategory = _context.CoffeeMenu.Where(p => p.ItemCategories.Any(pc => pc.CategoryId == categoryId)).ToList();
            var categories = _context.CoffeeCategories.ToList();
            return View(productsInCategory);


        }
    }
}
    
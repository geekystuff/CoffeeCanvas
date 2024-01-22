using CoffeeCanvas.Data;
using CoffeeCanvas.Models;
using CoffeeCanvas.ViewModels;
using CoffeeCanvas.Views.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CoffeeCanvas.Controllers
{
    public class CartController : Controller
    {
        private readonly IApplicationDbContext _context;

        public CartController(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public JsonResult AddToCart(int menuId)
        {
            var user_Id = HttpContext.Session.GetString("userId");

            
            var existingCartItem = _context.Carts
                .FirstOrDefault(x => x.MenuId == menuId && x.UserId.ToString() == user_Id);

            if (existingCartItem != null)
            {
                
                existingCartItem.Quantity += 1;
            }
            else
            {
                
                var itemDetails = _context.ItemCategories.FirstOrDefault(x => x.MenuId == menuId);
                var itemPrice = _context.CoffeeMenu.FirstOrDefault(x => x.MenuId == menuId);

                if (itemDetails != null && itemPrice != null)
                {
                    var cartItem = new ShoppingCart
                    {
                        MenuId = menuId,

                        CategoryId = itemDetails.CategoryId,
                        TotalPrice = itemPrice.Price,
                        Quantity = 1,
                        UserId = Convert.ToInt32(user_Id)
                    };

                    _context.Carts.Add(cartItem);
                }
                else
                {
                    return Json(new { success = false, error = "Items not found" });
                }
            }

            _context.SaveChanges();

            return Json(new { success = true });


        }

        public int GetTotalPrice()
        {
            var userId = HttpContext.Session.GetString("userId");
            var total_price = 0;
            

            var cartItems = _context.Carts
                .Where(cart => cart.UserId.ToString() == userId)
                .ToList();


           
            foreach (var product in cartItems)
            {
                var price = product.TotalPrice * product.Quantity;
                total_price += price;
                
            }
            return total_price;
            

        }
        public JsonResult GetCartItemCount()
        {
            var userId = HttpContext.Session.GetString("userId");

            
            var itemCount = _context.Carts
                .Where(x => x.UserId.ToString() == userId)
                .Sum(x => x.Quantity);

            return Json(new { count = itemCount });
        }

        
        public IActionResult CartView()
        {
            var userId = HttpContext.Session.GetString("userId");
            
            var total_quantity = 0;
            
            var cartItems = _context.Carts
                .Where(cart => cart.UserId.ToString() == userId)
               .ToList();


            var itemNames = _context.CoffeeMenu
                .Where(menu => cartItems.Select(cart => cart.MenuId).Contains(menu.MenuId))
                .ToDictionary(menu => menu.MenuId, menu => menu.CoffeeName);


            var cartViewModel = cartItems.Select(cartItem => new CartItemViewModel
            {
                MenuId = cartItem.MenuId,
                ItemName = itemNames.GetValueOrDefault(cartItem.MenuId),
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.TotalPrice
            }).ToList();
            foreach (var product in cartViewModel)
            {
                
                total_quantity += product.Quantity;
            }
            GetTotalPrice();
            
            ViewBag.ItemCount = total_quantity;
            return View("CartView", cartViewModel);
        }

        public IActionResult ClearCart()
        {
            var userId = HttpContext.Session.GetString("userId");
            var cartItems = _context.Carts.Where(c=>c.UserId.ToString() == userId).ToList();
            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();
            return RedirectToAction("Index","Menu");
        }

        public IActionResult RemoveFromCart(int menuId)
        {
            var user_id = HttpContext.Session.GetString("userId");
            var _cartItem = _context.Carts.FirstOrDefault(x=>x.MenuId== menuId && x.UserId.ToString()==user_id);
            if (_cartItem != null)
            {
                _context.Carts.Remove(_cartItem);
                _context.SaveChanges();
                return RedirectToAction("CartView");
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public JsonResult UpdateCartItemQuantity(CartItemQuantity model)
        {
            
            var userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, error = "User not authenticated" });
            }

            var cartItem = _context.Carts.FirstOrDefault(c => c.MenuId == model.menuId && c.UserId.ToString() == userId);

            if (cartItem == null)
            {
                return Json(new { success = false, error = "Cart item not found" });
            }

           
            cartItem.Quantity = model.newQuantity;
            

            
            _context.Carts.Update(cartItem);
            _context.SaveChanges();

            return Json(new { success = true, total_price = GetTotalPrice() }) ;
        }











    }

}

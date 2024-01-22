using CoffeeCanvas.Data;
using CoffeeCanvas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using CoffeeCanvas.ViewModels;

namespace CoffeeCanvas.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public AccountController(IApplicationDbContext context,IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }
        [HttpGet]
        public IActionResult Register()
        {
            var user_Id = HttpContext.Session.GetString("userId");
            if (user_Id != null)
            {
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(ApplicationUser model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid Request.";
                return View();
            }
            else
            {
                var existing_user = _context.Users.Where(x => x.UserName == model.UserName).FirstOrDefault();
                var existingEmail = _context.Users.Any(x => x.Email == model.Email);
                if (existing_user != null){
                    TempData["ErrorMessage"]= "This account already exists.";
                    return View();
                }
                
                else if (existingEmail)
                {
                    TempData["ErrorMessage"] = "This email is already associated with an account.";
                    return View();
                }
                else
                {
                    _context.Users.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                
            }
            

        }
        public IActionResult Login()
        {
            var user_Id = HttpContext.Session.GetString("userId");
            if (user_Id != null)
            {
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }

        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }

        [HttpPost]

        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName == model.Username);

                if (user != null && user.Password == model.Password)
                {
                   
                    HttpContext.Session.SetString("userName", model.Username);
                    HttpContext.Session.SetString("userId", user.UserId.ToString());
                    

                    
                    return RedirectToAction("Index", "Menu");
                }
                else if(user==null) {
                    TempData["ErrorMessage"] = "User not registered.";

                }
                else
                {
                    
                    TempData["ErrorMessage"] = "Incorrect Password. Please enter again.";
                }
            }
            var applicationUser = new ApplicationUser { UserName = model.Username };

            return View(applicationUser);

            

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            
            return RedirectToAction("Index","Home");
        }

    }
}
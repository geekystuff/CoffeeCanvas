using CoffeeCanvas.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCanvas.Data
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<Menu> CoffeeMenu { get; set; }
        DbSet<Category> CoffeeCategories { get; set; }
        DbSet<ItemCategory> ItemCategories { get; set; }
        DbSet<ShoppingCart> Carts { get; set; }
        DbSet<Orders> CoffeeOrders { get; set; }
        int SaveChanges();
    }
}

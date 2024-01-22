using CoffeeCanvas.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCanvas.Data
{
    public class ApplicationDbContext:DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Menu> CoffeeMenu { get; set; }
        public DbSet<Category> CoffeeCategories { get; set;}
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<Orders> CoffeeOrders { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ApplicationUser>(entity =>
        //    {
        //        entity.HasKey(e => e.UserId);
        //        entity.Property(e => e.UserId)
        //            .ValueGeneratedOnAdd();
        //        entity.Property(e => e.Email).IsRequired();
        //        entity.Property(e => e.FirstName).IsRequired();
        //        entity.Property(e => e.LastName).IsRequired();
        //        entity.Property(e => e.Password).IsRequired();
        //        entity.Property(e => e.UserName).IsRequired();
        //        entity.ToTable("Users");
        //    });

        //    // Add any additional model configurations here

        //    base.OnModelCreating(modelBuilder);
        //}*/
    }

}


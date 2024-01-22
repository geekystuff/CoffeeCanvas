using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeCanvas.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [Required]
        public string? CoffeeName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string? ImageFileName { get; set; }

        
        public List<ItemCategory>? ItemCategories { get; set; }
    }

}

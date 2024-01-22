using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeCanvas.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }

        
        public int MenuId { get; set; }
        public int CategoryId { get; set; }

        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int UserId { get; set; }

        
        public Menu? Menu { get; set; }
        public Category? Category { get; set; }
    }

}

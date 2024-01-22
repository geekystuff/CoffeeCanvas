using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeCanvas.Models
{
    [Table("ItemCategory")]
    public class ItemCategory
    {
        [Key]
        public int Id { get; set; }

        
        public int MenuId { get; set; }
        public int CategoryId { get; set; }

        
        public Menu? Menu { get; set; }
        public Category? Category { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeCanvas.Models
{
    [Table("Orders")]
    public class Orders
    {

    


        [Key]
        public int OrdersId { get; set; }
    }
}

namespace CoffeeCanvas.ViewModels
{
    public class CartItemViewModel
    {
        public int MenuId { get; set; }
        public object ItemName { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
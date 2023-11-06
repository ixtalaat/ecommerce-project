namespace Ecommerce.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public required IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double OrderTotal { get; set; }
    }
}

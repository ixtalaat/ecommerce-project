namespace Ecommerce.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderHeader OrderHeader { get; set; } = null!;
        public IEnumerable<OrderDetail> OrderDetail { get; set; } = null!;
    }
}

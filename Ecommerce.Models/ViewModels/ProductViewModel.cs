using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = null!;
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}

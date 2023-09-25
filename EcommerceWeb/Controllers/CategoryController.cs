using EcommerceWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.OrderBy(c => c.DisplayOrder).ToList();
            return View(categories);
        }
    }
}

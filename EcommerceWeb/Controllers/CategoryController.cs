using Ecommerce.DataAccess.Data;
using Ecommerce.Models;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([FromForm] Category category)
        {
            //if (category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "Display Order cannot exactly match the Name.");
            //}

			if (!ModelState.IsValid)
			{
				return View("Create", category);
			}

			_context.Categories.Add(category);
            _context.SaveChanges();

            TempData["success"] = "Category created Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null)
                return BadRequest();

            var categoryFromDb = _context.Categories.Find(id);

            if (categoryFromDb is null)
                return NotFound();

            return View(categoryFromDb);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromForm] Category category)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", category);
			}

			var categoryToUpdate = _context.Categories.Find(category.Id);

            if (categoryToUpdate is null)
                return NotFound();

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.DisplayOrder = category.DisplayOrder;
            
            _context.SaveChanges();

			TempData["success"] = "Category updated Successfully";

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete([FromRoute] int? id)
		{
			if (id is null)
				return BadRequest();

			var categoryFromDb = _context.Categories.Find(id);

			if (categoryFromDb is null)
				return NotFound();

			return View(categoryFromDb);
		}
        [HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST([FromForm] int? id)
		{
			if (id is null)
				return BadRequest();

			var categoryToDelete = _context.Categories.Find(id);

			if (categoryToDelete is null)
				return NotFound();

            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();

			TempData["success"] = "Category deleted Successfully";

			return RedirectToAction(nameof(Index));
		}
	}
}

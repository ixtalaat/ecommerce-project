using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
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
			if (!ModelState.IsValid)
			{
				return View("Create", category);
			}

            _categoryRepository.Add(category);
            _categoryRepository.Save();

            TempData["success"] = "Category created Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null)
                return BadRequest();

            //var categoryFromDb = _context.Categories.Find(id);
            var categoryFromDb = _categoryRepository.Get(c => c.Id == id);

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

			var categoryToUpdate = _categoryRepository.Get(c => c.Id == category.Id);


            if (categoryToUpdate is null)
                return NotFound();

            _categoryRepository.Update(categoryToUpdate);
            _categoryRepository.Save();

			TempData["success"] = "Category updated Successfully";

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete([FromRoute] int? id)
		{
			if (id is null)
				return BadRequest();

            var categoryFromDb = _categoryRepository.Get(c => c.Id == id);


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

            var categoryToDelete = _categoryRepository.Get(c => c.Id == id);


            if (categoryToDelete is null)
                return NotFound();

            _categoryRepository.Remove(categoryToDelete);
            _categoryRepository.Save();

			TempData["success"] = "Category deleted Successfully";

			return RedirectToAction(nameof(Index));
		}
	}
}

using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll();
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

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();

            TempData["success"] = "Category created Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null)
                return BadRequest();

            var categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);

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

            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();

            TempData["success"] = "Category updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int? id)
        {
            if (id is null)
                return BadRequest();

            var categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);


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

            var categoryToDelete = _unitOfWork.Category.Get(c => c.Id == id);


            if (categoryToDelete is null)
                return NotFound();

            _unitOfWork.Category.Remove(categoryToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Category deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}

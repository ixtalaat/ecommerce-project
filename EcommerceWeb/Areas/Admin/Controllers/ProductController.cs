using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models;
using Ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.ProductRepository.GetAll(includeProperties:"Category");
            
            return View(products);
        }
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            if(id is null)
            {
                //Create
                return View(productViewModel);
            }
            else
            {
                //Update
                productViewModel.Product = _unitOfWork.ProductRepository.Get(c => c.Id == id);
                return View(productViewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert([FromForm] ProductViewModel productViewModel, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View("Upsert", productViewModel);
            }

            var wwwRoot = _webHostEnvironment.WebRootPath;
            if (file is not null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var productPath = Path.Combine(wwwRoot, @"images\product");

                if(!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                {
                    //Delete the old image
                    var oldImagePath = Path.Combine(wwwRoot, productViewModel.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (FileStream fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productViewModel.Product.ImageUrl = @"\images\product\" + fileName;
            }

            if (productViewModel.Product.Id == 0)
            {
                _unitOfWork.ProductRepository.Add(productViewModel.Product);

            }
            else
            {
                _unitOfWork.ProductRepository.Update(productViewModel.Product);
            }

            _unitOfWork.Save();
            TempData["success"] = "Product created Successfully";
            return RedirectToAction(nameof(Index));
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category");
            return Json(new { data = products });
        }
        public IActionResult Delete(int? id)
        {
            var productToDelete = _unitOfWork.ProductRepository.Get(c => c.Id == id);
            if(productToDelete is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToDelete.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ProductRepository.Remove(productToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}

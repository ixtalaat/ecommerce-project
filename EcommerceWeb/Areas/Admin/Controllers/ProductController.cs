﻿using Ecommerce.DataAccess.IRepository;
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

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.ProductRepository.GetAll();
            
            return View(products);
        }
        public IActionResult Create()
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

            return View(productViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View("Create", productViewModel);
            }

            _unitOfWork.ProductRepository.Add(productViewModel.Product);
            _unitOfWork.Save();

            TempData["success"] = "Product created Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null)
                return BadRequest();

            var productFromDb = _unitOfWork.ProductRepository.Get(c => c.Id == id);

            if (productFromDb is null)
                return NotFound();

            return View(productFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", product);
            }

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();

            TempData["success"] = "Product updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int? id)
        {
            if (id is null)
                return BadRequest();

            var productFromDb = _unitOfWork.ProductRepository.Get(c => c.Id == id);


            if (productFromDb is null)
                return NotFound();

            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST([FromForm] int? id)
        {
            if (id is null)
                return BadRequest();

            var productToDelete = _unitOfWork.ProductRepository.Get(c => c.Id == id);


            if (productToDelete is null)
                return NotFound();

            _unitOfWork.ProductRepository.Remove(productToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
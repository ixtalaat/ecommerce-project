using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models.ViewModels;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IActionResult Index()
        {
            var companies = _unitOfWork.CompanyRepository.GetAll();

            return View(companies);
        }
        public IActionResult Upsert(int? id)
        {
            if (id is null)
            {
                //Create
                return View(new Company());
            }
            else
            {
                //Update
                var company = _unitOfWork.CompanyRepository.Get(c => c.Id == id);
                return View(company);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert([FromForm] Company company)
        {
            if (!ModelState.IsValid)
            {
                return View("Upsert", company);
            }

            if (company.Id == 0)
            {
                _unitOfWork.CompanyRepository.Add(company);

            }
            else
            {
                _unitOfWork.CompanyRepository.Update(company);
            }

            _unitOfWork.Save();
            TempData["success"] = "Company created Successfully";
            return RedirectToAction(nameof(Index));
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _unitOfWork.CompanyRepository.GetAll();
            return Json(new { data = companies });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToDelete = _unitOfWork.CompanyRepository.Get(c => c.Id == id);
            if (companyToDelete is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.CompanyRepository.Remove(companyToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}

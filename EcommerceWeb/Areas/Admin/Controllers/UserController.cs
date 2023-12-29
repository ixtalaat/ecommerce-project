using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models.ViewModels;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EcommerceWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult RoleManagment(string userId)
        {
            var roleId = _context.UserRoles.FirstOrDefault(u => u.UserId == userId)!.RoleId;

            var roleManagmentViewModel = new RoleManagmentViewModel()
            {
                ApplicationUser = _context.ApplicationUsers.Include(u => u.Company).FirstOrDefault(u => u.Id == userId)!,
                RoleList = _context.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _context.Companies.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            }; 

            roleManagmentViewModel.ApplicationUser.Role = _context.Roles.FirstOrDefault(u => u.Id == roleId)!.Name;

            return View(roleManagmentViewModel);
        }

        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentViewModel roleManagmentViewModel)
        {
            var roleId = _context.UserRoles.FirstOrDefault(u => u.UserId == roleManagmentViewModel.ApplicationUser.Id)!.RoleId;
            var oldRole = _context.Roles.FirstOrDefault(u => u.Id == roleId)!.Name;

            if (!(roleManagmentViewModel.ApplicationUser.Role == oldRole))
            {
                // a role was changed
                ApplicationUser applicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == roleManagmentViewModel.ApplicationUser.Id)!;
                if (roleManagmentViewModel.ApplicationUser.Role == StaticDetails.Role_Company)
                {
                    applicationUser.CompanyId = roleManagmentViewModel.ApplicationUser.CompanyId;
                }
                if (oldRole == StaticDetails.Role_Employee)
                {
                    applicationUser.CompanyId = null;
                }

                _context.SaveChanges();
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole!).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagmentViewModel.ApplicationUser.Role!).GetAwaiter().GetResult();
            
            }

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.ApplicationUsers.Include(u => u.Company).ToList();

            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();

            foreach (var user in users)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id)!.RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId)!.Name; 

                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }

            return Json(new { data = users });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                // user is currently locked, we will unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            _context.SaveChanges();

            return Json(new { success = true, message = "Operation Successful" });
        }
        #endregion
    }
}

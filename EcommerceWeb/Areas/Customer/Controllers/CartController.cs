using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models;
using Ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCarts = _unitOfWork.ShoppingCartRepository.GetAll(s => s.ApplicationUserId == userId, includeProperties: "Product");

            ShoppingCartViewModel = new()
            {
                ShoppingCartList = shoppingCarts,
                OrderTotal = shoppingCarts.Sum(c => (c.Product.ListPrice * c.Count))
            };
            
            return View(ShoppingCartViewModel);
        }
    }
}

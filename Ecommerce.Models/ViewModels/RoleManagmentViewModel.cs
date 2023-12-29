using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Models.ViewModels
{
    public class RoleManagmentViewModel
    {
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public IEnumerable<SelectListItem> RoleList { get; set; } = null!;
        public IEnumerable<SelectListItem> CompanyList { get; set; } = null!;

    }
}

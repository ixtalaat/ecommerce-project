using Ecommerce.Models;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser> 
    {
        void Update(ApplicationUser applicationUser);
    }
}

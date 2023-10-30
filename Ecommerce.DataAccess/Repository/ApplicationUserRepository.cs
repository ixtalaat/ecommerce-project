using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        } 
    }
}

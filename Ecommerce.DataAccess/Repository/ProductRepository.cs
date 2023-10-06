using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product obj)
        {
            _context.Products.Update(obj);
        }
    }
}

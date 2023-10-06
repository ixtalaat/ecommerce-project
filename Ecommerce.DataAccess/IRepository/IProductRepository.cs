using Ecommerce.Models;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IProductRepository : IRepository<Product> 
    {
        void Update(Product obj);
    }

}

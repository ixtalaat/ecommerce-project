using Ecommerce.Models;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage> 
    {
        void Update(ProductImage obj);
    }
}

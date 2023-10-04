using Ecommerce.Models;

namespace Ecommerce.DataAccess.IRepository
{
    public interface ICategoryRepository : IRepository<Category> 
    {
        void Update(Category obj);
    }
}

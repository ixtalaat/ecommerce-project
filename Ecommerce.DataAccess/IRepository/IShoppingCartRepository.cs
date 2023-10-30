using Ecommerce.Models;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart> 
    {
        void Update(ShoppingCart obj);
    }
}

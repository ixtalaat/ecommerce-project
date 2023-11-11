using Ecommerce.Models;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader> 
    {
        void Update(OrderHeader obj);
    }
}

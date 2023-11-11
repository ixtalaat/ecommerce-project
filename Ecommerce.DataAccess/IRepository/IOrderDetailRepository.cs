using Ecommerce.Models;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail> 
    {
        void Update(OrderDetail obj);
    }
}

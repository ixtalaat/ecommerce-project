using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.IRepository;
using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetail obj)
        {
            _context.OrderDetails.Update(obj);
        }
       
    }
}

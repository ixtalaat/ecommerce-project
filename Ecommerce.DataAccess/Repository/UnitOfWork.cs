using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.IRepository;

namespace Ecommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            CategoryRepository = new CategoryRepository(context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

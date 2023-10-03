using System.Linq.Expressions;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T -> Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(int id);
        void RemoveRange(IEnumerable<T> entities);
    }
}

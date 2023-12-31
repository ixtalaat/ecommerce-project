﻿using System.Linq.Expressions;

namespace Ecommerce.DataAccess.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T -> Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

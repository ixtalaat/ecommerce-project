﻿using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.IRepository;

namespace Ecommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
﻿using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;
using ParrotdiseShop.Persistence.Repositories;

namespace ParrotdiseShop.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Products = new ProductRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}

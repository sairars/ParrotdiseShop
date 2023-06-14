using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;
using ParrotdiseShop.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParrotdiseShop.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository Categories { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}

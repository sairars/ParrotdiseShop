using Microsoft.EntityFrameworkCore;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProductsWithCategory()
        {
            return _context.Products.Include(p => p.Category);
        }

        public IEnumerable<Product> GetProductsBy(int categoryId)
        {
            return _context.Products.Where(p => categoryId == 0 || p.CategoryId == categoryId);
        }

        public Product? GetProductWithCategory(int id)
        {
            return _context.Products
                                    .Include(p => p.Category)
                                    .SingleOrDefault(p => p.Id == id);
        }
    }
}

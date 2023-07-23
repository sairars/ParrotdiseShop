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
            var products = _context.Products.Include(p => p.Category);
            return products;
        }

        public IEnumerable<Product> GetProductsByCategory(int id)
        {
            var products = _context.Products.Where(p => id == 0 || p.CategoryId == id);
            return products;
        }

        public Product? GetProductWithCategory(int id)
        {
            var product = _context.Products
                                    .Include(p => p.Category)
                                    .SingleOrDefault(p => p.Id == id);
            return product;
        }
    }
}

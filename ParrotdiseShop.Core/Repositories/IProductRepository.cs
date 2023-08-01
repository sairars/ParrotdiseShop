using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllProductsWithCategory();
        IEnumerable<Product> GetProductsBy(int categoryId);
        Product? GetProductWithCategory(int id);
    }
}

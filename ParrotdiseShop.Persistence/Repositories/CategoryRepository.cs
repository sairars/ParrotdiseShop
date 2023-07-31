using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}

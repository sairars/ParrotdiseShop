using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(ApplicationDbContext context) : base(context) { }
    }
}

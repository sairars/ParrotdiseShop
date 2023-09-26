using Microsoft.EntityFrameworkCore;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartItemRepository(ApplicationDbContext context) : base(context) 
        { 
            _context = context;
        }

        public IEnumerable<ShoppingCartItem> GetAllShoppingCartItemsWithProductsByCookie(string? guestcookieId)
        {
            return _context.ShoppingCartItems
                        .Where(sc => sc.GuestCookieId == guestcookieId)
                        .Include(sc => sc.Product);
        }

        public IEnumerable<ShoppingCartItem> GetAllShoppingCartItemsWithProductsByUser(string userId)
        {
            return _context.ShoppingCartItems
                        .Where(sc => sc.UserId == userId)
                        .Include(sc => sc.Product);
        }

        public ShoppingCartItem? GetShoppingCartItemWithProduct(int id)
        {
            return _context.ShoppingCartItems
                        .Include(sc => sc.Product)
                        .SingleOrDefault(sc => sc.Id == id);
        }
    }
}

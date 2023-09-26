using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.Repositories
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem>
    {
        IEnumerable<ShoppingCartItem> GetAllShoppingCartItemsWithProductsByUser(string userId);
        IEnumerable<ShoppingCartItem> GetAllShoppingCartItemsWithProductsByCookie(string? guestCookieId);

        ShoppingCartItem? GetShoppingCartItemWithProduct(int id);
    }
}

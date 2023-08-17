using ParrotdiseShop.Core.Repositories;

namespace ParrotdiseShop.Core
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IShoppingCartItemRepository ShoppingCartItems { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        void Complete();
    }
}

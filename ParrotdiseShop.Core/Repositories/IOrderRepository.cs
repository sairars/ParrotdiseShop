using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order? GetOrderDetailsWithUser(int id);

        IEnumerable<Order> GetAllOrdersWithUser();
    }
}

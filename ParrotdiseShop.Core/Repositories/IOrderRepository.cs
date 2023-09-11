using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order? GetOrderDetailsWithUser(int id);
        IEnumerable<Order> GetAllOrdersWithUser();
        IEnumerable<Order> GetOrdersWithUserBy(string status);
        IEnumerable<Order> GetUserOrdersBy(string status, string userId);
    }
}

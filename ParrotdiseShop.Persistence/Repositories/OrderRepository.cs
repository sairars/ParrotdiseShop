using Microsoft.EntityFrameworkCore;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
		private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
			_context = context;
        }

		public IEnumerable<Order> GetAllOrdersWithUser()
		{
			return _context.Orders.Include(o => o.User);
		}

		public Order? GetOrderDetailsWithUser(int id)
		{
			return _context.Orders
						.Include(o => o.User)
						.Include(o => o.OrderDetails)
						.SingleOrDefault(o => o.Id == id);
		}

        public IEnumerable<Order> GetOrdersWithUserBy(string status)
        {
			return _context.Orders
						.Include(o => o.User)
						.Where(o => (status.Equals("all", StringComparison.OrdinalIgnoreCase) 
									|| o.Status == status)
									&& o.Status != OrderStatus.StatusPending);
        }

        public IEnumerable<Order> GetUserOrdersBy(string status, string userId)
        {
            return _context.Orders
						.Include(o => o.User)
						.Where(o => (status.Equals("all", StringComparison.OrdinalIgnoreCase) 
										|| o.Status == status) 
									&& o.UserId == userId
									&& o.Status != OrderStatus.StatusPending);
        }
    }
}

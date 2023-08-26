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

		public Order? GetOrderWithUser(int id)
		{
			return _context.Orders
						.Include(o => o.User)
						.SingleOrDefault(o => o.Id == id);
		}
	}
}

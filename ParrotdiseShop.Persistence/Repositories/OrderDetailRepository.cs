using Microsoft.EntityFrameworkCore;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsBy(int orderId)
        {
            return _context.OrderDetails
                        .Include(od => od.Product)
                        .Where(od => od.OrderId == orderId);
        }
    }
}

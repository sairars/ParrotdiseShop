using ParrotdiseShop.Core.Models;
using System;
namespace ParrotdiseShop.Core.ViewModels
{
    public class OrdersViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}

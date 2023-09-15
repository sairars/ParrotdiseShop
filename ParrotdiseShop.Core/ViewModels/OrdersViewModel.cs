using Microsoft.AspNetCore.Mvc.Rendering;
using ParrotdiseShop.Core.Models;
namespace ParrotdiseShop.Core.ViewModels
{
    public class OrdersViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
    }
}

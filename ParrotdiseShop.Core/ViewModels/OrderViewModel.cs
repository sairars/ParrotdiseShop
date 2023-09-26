using Microsoft.AspNetCore.Mvc.Rendering;
using ParrotdiseShop.Core.Dtos;

namespace ParrotdiseShop.Core.ViewModels
{
    public class OrderViewModel
    {
        public OrderDto Order { get; set; }
        public IEnumerable<OrderDetailDto> OrderDetails { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
    }
}

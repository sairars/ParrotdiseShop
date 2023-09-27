using Microsoft.AspNetCore.Mvc.Rendering;
using ParrotdiseShop.Core.Dtos;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartItemDto> ShoppingCartItems { get; set; }
        public OrderDto Order { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public decimal Total
        {
            get
            {
                if (ShoppingCartItems == null)
                    return 0;

                return ShoppingCartItems
                            .Select(sc => new { TotalPerItem = sc.Product.UnitPrice * sc.Quantity })
                            .Sum(t => t.TotalPerItem);
            }
        }

        public bool IsValid
        {
            get
            {
                if (ShoppingCartItems == null)
                    return false;

                return !ShoppingCartItems.Any(sc => sc.Quantity > sc.Product.UnitsInStock);
            }
        }
    }
}

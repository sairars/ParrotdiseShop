using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartItemDto> ShoppingCartItems { get; set; }
        public Order Order { get; set; }
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

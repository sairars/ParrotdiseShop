using ParrotdiseShop.Core.Dtos;
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
        public decimal Total
        {
            get
            {
                return ShoppingCartItems
                            .Select(sc => new { TotalPerItem = sc.Product.UnitPrice * sc.Quantity })
                            .Sum(t => t.TotalPerItem);
            }
        }
    }
}

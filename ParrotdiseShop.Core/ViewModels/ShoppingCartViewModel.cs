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
        public ShoppingCartItemDto ShoppingCartItemDto { get; set; }
        public decimal Price { get; set; }
    }
}

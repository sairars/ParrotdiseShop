using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ParrotdiseShop.Core.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ShoppingCartItemViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ValidateNever]
        public ProductDto Product { get; set; }

        [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
        public int Quantity { get; set; }
    }
}

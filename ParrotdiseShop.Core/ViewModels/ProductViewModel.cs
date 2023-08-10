using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}

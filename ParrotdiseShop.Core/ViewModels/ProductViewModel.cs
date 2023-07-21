using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ParrotdiseShop.Core.Dtos;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<ProductDto> ProductDtos { get; set; }
        public IEnumerable<CategoryDto> CategoryDtos { get; set; }
    }
}

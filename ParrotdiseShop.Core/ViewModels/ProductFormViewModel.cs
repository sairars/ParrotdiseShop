using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ProductFormViewModel
    {
        public ProductDto Product { get; set; }

        [ValidateNever]
        public IEnumerable<CategoryDto> Categories { get; set; }

        [ValidateNever]
        public string Heading { get; set; }
        public bool IsEdit { get; set; }
    }
}

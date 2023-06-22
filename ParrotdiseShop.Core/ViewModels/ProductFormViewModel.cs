using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ProductFormViewModel
    {
        public ProductDto? ProductDto { get; set; }

        [ValidateNever]
        public IEnumerable<CategoryDto> CategoryDtos { get; set; }

        [ValidateNever]
        public string Heading { get; set; }
        public bool IsEdit { get; set; }
    }
}

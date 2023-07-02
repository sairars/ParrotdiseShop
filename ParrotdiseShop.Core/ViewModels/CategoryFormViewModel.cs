using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ParrotdiseShop.Core.Dtos;

namespace ParrotdiseShop.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        public CategoryDto? Category { get; set; }

        [ValidateNever]
        public string Heading { get; set; }
        public bool IsEdit { get; set; }
    }
}

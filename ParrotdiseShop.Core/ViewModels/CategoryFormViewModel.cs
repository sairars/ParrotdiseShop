using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        public Category Category { get; set; }

        [ValidateNever]
        public string Heading { get; set; }
        public bool IsEdit { get; set; }
    }
}

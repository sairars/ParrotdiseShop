using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(20)]
        public string? SKU { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Units In Stock")]
        public int UnitsInStock { get; set; }

        [ValidateNever]
        [Display(Name = "Product Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public CategoryDto Category { get; set; }
    }
}

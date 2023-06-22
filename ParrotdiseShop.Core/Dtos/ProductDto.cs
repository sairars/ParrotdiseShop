using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ParrotdiseShop.Core.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }

        [ValidateNever]
        public CategoryDto Category { get; set; }
    }
}

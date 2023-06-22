using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(20)]
        public string? SKU { get; set; }

        [Range(1, 1000)]
        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

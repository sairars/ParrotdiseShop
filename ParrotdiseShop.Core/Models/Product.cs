using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(20)]
        public string? SKU { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public string ImagePath { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public void Increment(int unitsInStock)
        {
            UnitsInStock += unitsInStock;
        }

        public void Decrement(int unitsInStock)
        {
            UnitsInStock -= unitsInStock;
        }
    }
}

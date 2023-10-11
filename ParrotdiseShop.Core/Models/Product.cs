﻿namespace ParrotdiseShop.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? SKU { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public string ImagePath { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Dtos
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }

        [Range(1, 1000, ErrorMessage ="Must be between 1 and 1000")]
        public int Quantity { get; set; }
    }
}

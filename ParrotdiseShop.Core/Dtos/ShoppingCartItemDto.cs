using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.Dtos
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public string GuestCookieId { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}

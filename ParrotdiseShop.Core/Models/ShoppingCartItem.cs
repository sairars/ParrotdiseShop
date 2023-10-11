namespace ParrotdiseShop.Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string? GuestCookieId { get; set; }
        public string? UserId  { get; set; }
        public ApplicationUser? User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }

        public void Increment(int quantity)
        {
            Quantity += quantity;
        }

        public void Decrement()
        {
            Quantity--;
        }

        public void UpdateUserId(string userId)
        {
            UserId = userId;
        }
    }
}

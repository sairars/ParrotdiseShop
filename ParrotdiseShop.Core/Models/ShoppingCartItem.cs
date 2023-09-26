using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public string? GuestCookieId { get; set; }

        [ValidateNever]
        public string? UserId  { get; set; }

        [ValidateNever]
        public ApplicationUser? User { get; set; }

        public int ProductId { get; set; }

        [ValidateNever]
        public Product Product { get; set; }

        [Range(1,1000, ErrorMessage = "Must be between 1 and 1000")]
        public int Quantity { get; set; }

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

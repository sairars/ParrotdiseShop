using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string UserId  { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Range(1,1000, ErrorMessage = "Must be between 1 and 1000")]
        public int Quantity { get; set; }        

        public void Update(int quantity)
        {
            Quantity += quantity;
        }
    }
}

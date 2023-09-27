using Microsoft.AspNetCore.Identity;
using ParrotdiseShop.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Dtos
{
    public class OrderDto
    {
		public int Id { get; set; }

        [Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Street Address")]
		public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }

        public string Name { get; set; }

        public string GuestCookieId { get; set; }

        public string UserId { get; set; }

        public ApplicationUserDto User { get; set; }

        public DateTime CreationDate { get; set; }
        
        public decimal Total { get; set; }
        
        public string Status { get; set; }
        
        public string? PaymentStatus { get; set; }

		public DateTime ShippingDate { get; set; }

		public string? TrackingNumber { get; set; }

		public string? Carrier { get; set; }

		public DateTime PaymentDate { get; set; }

		public string? PaymentSessionId { get; set; }

		public string? PaymentIntentId { get; set; }
	}
}

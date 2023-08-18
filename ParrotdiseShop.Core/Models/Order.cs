namespace ParrotdiseShop.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentSessionId { get; set; }
        public string PaymentIntentId { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }

        public Order()
        {     
        }

        public Order(ApplicationUser customer)
        {
            Name = customer.Name;
            PhoneNumber = customer.PhoneNumber;
            City = customer.City;
            Province = customer.Province;
            PostalCode = customer.PostalCode;
            StreetAddress = customer.StreetAddress;
        }

    }
}

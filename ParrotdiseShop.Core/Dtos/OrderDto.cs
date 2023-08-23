using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParrotdiseShop.Core.Dtos
{
    public class OrderDto
    {
		public int Id { get; set; }
		public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string? PaymentStatus { get; set; }
    }
}

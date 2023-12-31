﻿using System.Collections.ObjectModel;
using ParrotdiseShop.Core.Utilities;

namespace ParrotdiseShop.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? GuestCookieId { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentSessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }
        public string?  Email { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; } = new Collection<OrderDetail>();

        public Order()
        {     
        }

        public Order(ApplicationUser customer)
        {
            Name = customer.Name;
            Email = customer.Email;
            PhoneNumber = customer.PhoneNumber;
            City = customer.City;
            Province = customer.Province;
            PostalCode = customer.PostalCode;
            StreetAddress = customer.StreetAddress;
        }

        public void Create(string? userId, string? guestCookieId, decimal total)
        {
            UserId = userId;
            GuestCookieId = guestCookieId;
            Total = total;
            Status = OrderStatus.StatusPending;
            PaymentStatus = OrderStatus.PaymentStatusPending;
            CreationDate = DateTime.Now;
        }

        public void UpdateStripeSessionId(string sessionId)
		{
            PaymentSessionId = sessionId;
		}

        public void UpdateStripePaymentId(string paymentId)
        {
            if (PaymentSessionId == null)
                return;

            PaymentIntentId = paymentId;
            PaymentDate = DateTime.Now;
        }

        public void UpdateStatus(string status, string? paymentStatus = null)
        {
            Status = status;

            if (paymentStatus != null)
                PaymentStatus = paymentStatus;
        }

        public void UpdateCustomerInformation(Order order)
        {
            Name = order.Name;
            PhoneNumber = order.PhoneNumber;
            StreetAddress = order.StreetAddress;
            City = order.City;
            Province = order.Province;
            PostalCode = order.PostalCode;
        }

        public void UpdateShippingInformation(string carrier, string trackingNumber)
        {
            ShippingDate = DateTime.Now;
            Status = OrderStatus.StatusShipped;
            Carrier = carrier;
            TrackingNumber = trackingNumber;
        }
    }
}

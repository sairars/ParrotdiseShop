namespace ParrotdiseShop.Core.Models
{
    public class OrderStatus
    {
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusProcessing = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusDisabled = "Disabled";
        public const string PaymentStatusPending = "PaymentPending";
		public const string PaymentStatusApproved = "PaymentApproved";
	}
}

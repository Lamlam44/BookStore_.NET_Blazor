using System.Collections.Generic;

namespace StoreManagement.Client.Models
{
    public class CreateInvoiceRequest
    {
        public string OrderType { get; set; } = "ONLINE";
        public string PaymentMethod { get; set; } = "CASH";
        public string? Phone { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PaymentNote { get; set; }

        // Match server DTO: payment/status fields are required by server validation
        public string PaymentStatus { get; set; } = string.Empty; // PAID | UNPAID
        public string Status { get; set; } = string.Empty; // PENDING | SHIPPED | DELIVERED | CANCELLED
        public decimal AmountPaid { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public string? VoucherId { get; set; }
        public string? CustomerId { get; set; }

        public List<CreateInvoiceDetailRequest> Details { get; set; } = new List<CreateInvoiceDetailRequest>();
    }

    public class CreateInvoiceDetailRequest
    {
        public string BookId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

using System.Collections.Generic;

namespace StoreManagement.Client.Models
{
    public class CreateInvoiceRequest
    {
        public string OrderType { get; set; } = "";
        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string? CustomerId { get; set; }
        public string Status { get; set; } = "";
        public decimal DiscountAmount { get; set; }
        public string? VoucherId { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentNote { get; set; } = "";
        public List<InvoiceDetailDto> Details { get; set; } = new List<InvoiceDetailDto>();
        public string Phone { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
namespace StoreManagement.Client.Models
{
    public enum InvoiceStatus
    {
        Pending,
        Paid,
        Cancelled
    }

    public class Invoice : BaseEntity
    {
        public string CustomerId { get; set; }
        public string CashierStaffId { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public DateTime PaymentTime { get; set; }
        public decimal Subtotal { get; set; }
        public string? VoucherCode { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending; // Default to Pending
        public virtual Account CashierStaff { get; set; } = default!;
        public virtual Voucher? Voucher { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}

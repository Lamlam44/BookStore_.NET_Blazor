namespace StoreManagement.Client.Models
{
    public class InvoiceDetailDto
    {
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}

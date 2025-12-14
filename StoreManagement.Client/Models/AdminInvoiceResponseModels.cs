namespace StoreManagement.Client.Models
{
    // Nested DTOs for InvoiceResponse/InvoiceDetailResponse
    public class AdminCustomerDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class AdminStaffDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PositionName { get; set; } = string.Empty;
    }

    public class AdminSummaryInvoiceDetailDTO
    {
        public string Id { get; set; } = string.Empty;
        public string BookId { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalDiscount { get; set; }
    }

    // InvoiceResponse for list view
    public class AdminInvoiceResponse
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string OrderType { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTime? PaymentTime { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal ChangeDue { get; set; }
        public string? PaymentNote { get; set; }
        public AdminCustomerDTO? Customer { get; set; }
        public AdminStaffDTO? Staff { get; set; }
        public List<AdminSummaryInvoiceDetailDTO> Details { get; set; } = new List<AdminSummaryInvoiceDetailDTO>();
    }

    // InvoiceDetailDTO from Responses (for individual item in details list)
    public class AdminInvoiceDetailDTO
    {
        public string Id { get; set; } = string.Empty;
        public string BookId { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty; // Assuming this exists in backend DTO
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal FinalPrice { get; set; }
    }


    // InvoiceDetailResponse for detail view
    public class AdminInvoiceDetailResponse
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string OrderType { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTime? PaymentTime { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal ChangeDue { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? PaymentNote { get; set; }
        public AdminCustomerDTO? Customer { get; set; }
        public AdminStaffDTO? Staff { get; set; }
        public List<AdminInvoiceDetailDTO> Details { get; set; } = new List<AdminInvoiceDetailDTO>();
    }
}

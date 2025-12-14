using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Client.Models
{
    public class AdminInvoiceDetailItem
    {
        [Required(ErrorMessage = "Thông tin sản phẩm là bắt buộc")]
        public string BookId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số lượng sản phẩm là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int Quantity { get; set; }
        public string? VoucherId { get; set; }

        [Required(ErrorMessage = "Giảm giá là bắt buộc")]
        [Range(minimum: 0, double.MaxValue, ErrorMessage = "Giảm giá phải lớn hơn hoặc bằng 0.")]
        public decimal TotalDiscount { get; set; }
    }

    public class AdminCreateInvoiceRequest
    {
        [Required(ErrorMessage = "Loại đơn hàng (OrderType) là bắt buộc.")]
        [RegularExpression("^(ONLINE|POS)$", ErrorMessage = "Loại đơn hàng phải là 'ONLINE' hoặc 'POS'.")]
        public string OrderType { get; set; } = "ONLINE"; // Default to ONLINE

        [Required(ErrorMessage = "Phương thức thanh toán là bắt buộc.")]
        public string PaymentMethod { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty; // Will be set by backend if not provided

        public string? CustomerId { get; set; }

        public string Status { get; set; } = string.Empty; // Will be set by backend if not provided

        public decimal DiscountAmount { get; set; } = 0;

        public string? VoucherId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số tiền thanh toán phải lớn hơn hoặc bằng 0.")]
        public decimal AmountPaid { get; set; } = 0;

        public string PaymentNote { get; set; } = string.Empty;

        [Required(ErrorMessage = "Chi tiết đơn hàng là bắt buộc.")]
        [MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất một sản phẩm.")]
        public List<AdminInvoiceDetailItem> Details { get; set; } = new List<AdminInvoiceDetailItem>();
    }

    public class AdminUpdateInvoiceRequest
    {
        [Required(ErrorMessage = "Loại đơn hàng (OrderType) là bắt buộc.")]
        [RegularExpression("^(ONLINE|POS)$", ErrorMessage = "Loại đơn hàng phải là 'ONLINE' hoặc 'POS'.")]
        public string OrderType { get; set; } = "ONLINE"; // Default to ONLINE

        [Required(ErrorMessage = "Phương thức thanh toán là bắt buộc.")]
        public string PaymentMethod { get; set; } = string.Empty;

        public string? CustomerId { get; set; }

        public decimal DiscountAmount { get; set; } = 0;

        public string? VoucherId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số tiền thanh toán phải lớn hơn hoặc bằng 0.")]
        public decimal AmountPaid { get; set; } = 0;

        public string PaymentNote { get; set; } = string.Empty;

        [Required(ErrorMessage = "Chi tiết đơn hàng là bắt buộc.")]
        [MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất một sản phẩm.")]
        public List<AdminInvoiceDetailItem> Details { get; set; } = new List<AdminInvoiceDetailItem>();
    }

    public class AdminUpdateStatusInvoiceRequest
    {
        [Required(ErrorMessage = "Trạng thái đơn hàng là bắt buộc")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái thanh toán là bắt buộc")]
        public string PaymentStatus { get; set; } = string.Empty;
    }
}

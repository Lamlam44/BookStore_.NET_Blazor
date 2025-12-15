using System.Text.Json.Serialization;

namespace StoreManagement.Client.Models
{
    public class ReceiptDetail : BaseEntity
    {
        public string ReceiptId { get; set; } = string.Empty;
        public string BookId { get; set; } = string.Empty;

        // --- QUAN TRỌNG: Thêm trường này để UI hiển thị tên sách ---
        [JsonPropertyName("bookName")]
        public string BookTitle { get; set; } = string.Empty; 

        public int QuantityReceived { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalLineCost { get; set; }

        // --- QUAN TRỌNG: Bỏ hoặc comment dòng dưới để tránh lỗi JSON vòng lặp ---
        // public virtual InventoryReceipt Receipt { get; set; } = default!;

        // Để Book là nullable (?) để tránh lỗi null khi khởi tạo
        public Book? Book { get; set; }
    }
}
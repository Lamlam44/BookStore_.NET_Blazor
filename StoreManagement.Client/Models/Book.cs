using System.Text.Json.Serialization;

namespace StoreManagement.Client.Models
{
    public class Book : BaseEntity
    {
        // --- CÁC TRƯỜNG CƠ BẢN ---
        public string Title { get; set; } = string.Empty;
        public string Image { get; set; } = "https://firebasestorage.googleapis.com/v0/b/todo-app-1fe54.appspot.com/o/books-image%2Fno-image.jpg?alt=media&token=de5eea16-4de9-49eb-ba00-fa8946e41276";
        public string Isbn { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
        
        // Map dữ liệu tồn kho từ API
        [JsonPropertyName("stockCanBeSold")]
        public int StockQuantity { get; set; }
        
        public bool IsAvailable { get; set; }

        // --- CÁC TRƯỜNG BỊ THIẾU (GÂY RA LỖI) ---
        // Bạn phải thêm các dòng này vào để ProductList.razor hiểu được
        public string Status { get; set; } = "ACTIVE"; 
        
        public string CategoryName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;

        // --- CÁC TRƯỜNG LIÊN KẾT (ID) ---
        public string AuthorId { get; set; } = string.Empty;
        public string PublisherId { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;

        // --- TRƯỜNG CŨ (Giữ lại để tránh lỗi code cũ nếu có) ---
        public string Author { get; set; } = string.Empty; 
        public string Publisher { get; set; } = string.Empty;
        
        // Object liên kết
        public Category? Category { get; set; }
    }
}
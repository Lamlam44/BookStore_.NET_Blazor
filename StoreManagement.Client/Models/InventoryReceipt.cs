using System.Text.Json.Serialization;

namespace StoreManagement.Client.Models
{
    public class InventoryReceipt : BaseEntity
    {
        public string SupplierId { get; set; } = string.Empty;
        public virtual Supplier? Supplier { get; set; }
        public string SupplierName { get; set; } = string.Empty;

        // Dùng TotalCost để khớp với Backend Response
        public decimal TotalCost { get; set; } 
        
        // --- QUAN TRỌNG: Backend trả về "GRNStatus", phải dùng đúng tên này ---
        public string GRNStatus { get; set; } = "DRAFT";

        // Biến này để tương thích code cũ (nếu có), map từ GRNStatus sang
        public string Status => GRNStatus; 
        [JsonPropertyName("details")]
        public List<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
    }
}
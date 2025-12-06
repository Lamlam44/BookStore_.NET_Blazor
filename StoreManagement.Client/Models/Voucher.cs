using System.Text.Json.Serialization;
namespace StoreManagement.Client.Models
{
    public class Voucher : BaseEntity
    {
        [JsonPropertyName("code")]
        public string VoucherCode { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string VoucherName { get; set; } = string.Empty; 
        public string VoucherType { get; set; } = "Percentage";
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
namespace StoreManagement.Client.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; // Mã tác giả (khớp với SQL)
        public string Status { get; set; } = "ACTIVE";   // Khớp với SQL
    }
}
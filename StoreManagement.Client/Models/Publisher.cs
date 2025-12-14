namespace StoreManagement.Client.Models
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = "ACTIVE";
        public bool IsDeleted { get; set; }
    }
}
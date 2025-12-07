namespace StoreManagement.Client.Models
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime? Dob { get; set; }
        public bool IsActive { get; set; }
    }
}

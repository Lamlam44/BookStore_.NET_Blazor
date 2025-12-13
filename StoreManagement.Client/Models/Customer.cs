namespace StoreManagement.Client.Models
{
    public class Customer : BaseEntity
    {
        public string? Email { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime? Dob { get; set; }
        public bool IsActive { get; set; }
        // Additional fields returned by backend customer / user profile
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
    }
}

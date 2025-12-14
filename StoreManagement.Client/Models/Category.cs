namespace StoreManagement.Client.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public string Status { get; set; } = "ACTIVE";
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

namespace StoreManagement.Client.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = string.Empty;

        public string CategoryCode { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

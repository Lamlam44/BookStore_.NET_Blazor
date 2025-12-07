namespace StoreManagement.Client.Models
{
    public class PaginationResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
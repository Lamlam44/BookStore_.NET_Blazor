namespace StoreManagement.Client.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}

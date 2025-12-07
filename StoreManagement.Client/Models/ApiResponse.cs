namespace StoreManagement.Client.Models
{
    // Class 1: Dùng cho các danh sách hoặc object cụ thể (VD: ApiResponse<Book>)
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    // Class 2: Dùng cho các hàm không cần trả về Data (VD: ApiResponse)
    // Class này kế thừa class trên, mặc định T là bool
    public class ApiResponse : ApiResponse<bool> 
    { 
    }
}
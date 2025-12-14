using StoreManagement.Client.Models;

namespace StoreManagement.Client.Services
{
    public interface IStoreService
    {
        // ==========================================
        // 1. SÁCH (BOOKS)
        // ==========================================
        Task<List<Book>> GetBooksAsync();
        Task<Book?> GetBookByIdAsync(string id);
        Task<List<Book>> SearchBooksAsync(string keyword);
        Task<bool> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(string id);
        
        // ==========================================
        // 2. NHÀ CUNG CẤP (SUPPLIERS)
        // ==========================================
        Task<List<Supplier>> GetSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(string id);
        Task<Supplier?> GetSupplierWithHistoryAsync(string id);
        Task CreateSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task<List<Supplier>> SearchSuppliersAsync(string keyword);

        // ==========================================
        // 3. KHUYẾN MÃI (VOUCHERS)
        // ==========================================
        Task<List<Voucher>> GetVouchersAsync();
        Task<Voucher?> GetVoucherByIdAsync(string id);
        Task CreateVoucherAsync(Voucher voucher);
        Task UpdateVoucherAsync(Voucher voucher);
        Task DeleteVoucherAsync(string id);

        // ==========================================
        // 4. DANH MỤC (CATEGORIES)
        // ==========================================
        Task<List<Category>> GetAllCategoriesAsync();
        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(string id);

        // ==========================================
        // 5. TÁC GIẢ (AUTHORS)
        // ==========================================
        Task<List<Author>> GetAllAuthorsAsync();
        Task<bool> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(string id);

        // ==========================================
        // 6. NHÀ XUẤT BẢN (PUBLISHERS)
        // ==========================================
        Task<List<Publisher>> GetPublishersAsync();      // Dùng cho trang quản lý chính
        Task<List<Publisher>> GetAllPublishersAsync();   // Dùng cho dropdown (alias)
        Task<bool> CreatePublisherAsync(Publisher publisher);
        Task<bool> UpdatePublisherAsync(Publisher publisher);
        Task<bool> DeletePublisherAsync(string id);

        // ==========================================
        // 7. KHÁCH HÀNG (CUSTOMERS)
        // ==========================================
        Task<List<Customer>> GetCustomersAsync();
        Task CreateCustomerAsync(Customer customer);
        
        // Overload hỗ trợ phân trang & tạo nhanh (nếu code cũ cần)
        Task<ApiResponse<PaginationResponse<Customer>>> GetCustomersAsync(int pageNumber, int pageSize);
        Task<ApiResponse<bool>> CreateCustomerAsync(string name, string phone, string address);

        // ==========================================
        // 8. KHO HÀNG & HÓA ĐƠN (INVENTORY & DASHBOARD)
        // ==========================================
        Task<bool> CreateReceiptAsync(InventoryReceipt receipt);
        Task<List<Invoice>> GetInvoicesAsync(); // Dùng cho Dashboard
    }
}
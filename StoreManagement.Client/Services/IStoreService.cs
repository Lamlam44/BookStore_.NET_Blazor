using StoreManagement.Client.Models;

namespace StoreManagement.Client.Services
{
    public interface IStoreService
    {
        // Books
        Task<List<Book>> GetBooksAsync();
        Task<Book?> GetBookByIdAsync(string id);
        Task<List<Book>> SearchBooksAsync(string keyword);
        
        // Suppliers
        Task<List<Supplier>> GetSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(string id);
        Task<Supplier?> GetSupplierWithHistoryAsync(string id);
        Task CreateSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task<List<Supplier>> SearchSuppliersAsync(string keyword);

        // Vouchers
        Task<List<Voucher>> GetVouchersAsync();
        Task<Voucher?> GetVoucherByIdAsync(string id);
        Task CreateVoucherAsync(Voucher voucher);
        Task UpdateVoucherAsync(Voucher voucher);
        Task DeleteVoucherAsync(string id);

        // Categories
        Task<List<Category>> GetAllCategoriesAsync();

        // Authors & Publishers
        Task<List<Author>> GetAllAuthorsAsync();
        Task<List<Publisher>> GetAllPublishersAsync();

        // Customers
        Task<List<Customer>> GetCustomersAsync();
        Task CreateCustomerAsync(Customer customer);

        // Inventory
        Task<bool> CreateReceiptAsync(InventoryReceipt receipt);
    }
}
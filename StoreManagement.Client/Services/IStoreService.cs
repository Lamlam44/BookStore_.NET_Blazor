using StoreManagement.Client.Models;

namespace StoreManagement.Client.Services
{
    public interface IStoreService
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book?> GetBookByIdAsync(string id);
        
        Task<List<Supplier>> GetSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(string id);
        Task<Supplier?> GetSupplierWithHistoryAsync(string id);
        Task CreateSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task<List<Supplier>> SearchSuppliersAsync(string keyword);

        Task<List<Voucher>> GetVouchersAsync();
        Task<Voucher?> GetVoucherByIdAsync(string id);
        Task CreateVoucherAsync(Voucher voucher);
        Task UpdateVoucherAsync(Voucher voucher);
        Task DeleteVoucherAsync(string id);

        Task<bool> CreateReceiptAsync(InventoryReceipt receipt);
    }
}
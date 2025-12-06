using StoreManagement.Client.Models;

namespace StoreManagement.Client.Services
{
    public class MockDataService : IStoreService
    {
        // ==========================================
        // DỮ LIỆU GIẢ (Chỉ giữ lại Books và Vouchers)
        // ==========================================

        private static List<Category> _categories = new List<Category>
        {
            new Category { Id = "cat-1", CategoryName = "Tiểu thuyết" },
            new Category { Id = "cat-2", CategoryName = "Kinh tế" },
            new Category { Id = "cat-3", CategoryName = "Manga" }
        };

        private static List<Book> _books = new List<Book>
        {
            new Book { Id = "b1", Title = "Nhà Giả Kim", Author = "Paulo Coelho", Isbn = "978-1", RetailPrice = 79000, StockQuantity = 5, CategoryId = "cat-1", Category = _categories[0] },
            new Book { Id = "b2", Title = "Dạy Con Làm Giàu", Author = "Robert Kiyosaki", Isbn = "978-2", RetailPrice = 120000, StockQuantity = 0, CategoryId = "cat-2", Category = _categories[1] }
        };

        // ĐÃ XÓA LIST _suppliers

        private static List<Voucher> _vouchers = new List<Voucher>
        {
            new Voucher { Id = "v1", VoucherCode = "SALE10", VoucherType = "Percentage", DiscountValue = 10, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10), IsActive = true }
        };

        // ==========================================
        // IMPLEMENTATION (TRIỂN KHAI)
        // ==========================================

        // --- BOOKS (GIỮ NGUYÊN) ---
        public Task<List<Book>> GetBooksAsync() => Task.FromResult(_books);
        
        public Task<Book?> GetBookByIdAsync(string id) 
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }

        // --- SUPPLIERS (ĐÃ XÓA LOGIC - CHỈ GIỮ VỎ ĐỂ KHÔNG LỖI INTERFACE) ---
        public Task<List<Supplier>> GetSuppliersAsync() 
        {
            return Task.FromResult(new List<Supplier>()); // Trả về rỗng
        }

        public Task<Supplier?> GetSupplierByIdAsync(string id) 
        {
            return Task.FromResult<Supplier?>(null); // Trả về null
        }

        public Task<Supplier?> GetSupplierWithHistoryAsync(string id) 
        {
            return Task.FromResult<Supplier?>(null);
        }

        public Task<List<Supplier>> SearchSuppliersAsync(string keyword)
        {
            return Task.FromResult(new List<Supplier>());
        }

        public Task CreateSupplierAsync(Supplier supplier) 
        {
            return Task.CompletedTask; // Không làm gì cả
        }

        public Task UpdateSupplierAsync(Supplier supplier) 
        {
            return Task.CompletedTask;
        }

        public Task DeleteSupplierAsync(string id) 
        {
            return Task.CompletedTask;
        }

        // --- VOUCHERS (GIỮ NGUYÊN) ---
        public Task<List<Voucher>> GetVouchersAsync() => Task.FromResult(_vouchers);

        public Task<Voucher?> GetVoucherByIdAsync(string id)
        {
            var voucher = _vouchers.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(voucher);
        }

        public Task CreateVoucherAsync(Voucher voucher)
        {
            voucher.Id = Guid.NewGuid().ToString();
            _vouchers.Add(voucher);
            return Task.CompletedTask;
        }

        public Task UpdateVoucherAsync(Voucher voucher)
        {
            var existing = _vouchers.FirstOrDefault(x => x.Id == voucher.Id);
            if (existing != null)
            {
                existing.VoucherCode = voucher.VoucherCode;
                existing.VoucherType = voucher.VoucherType;
                existing.DiscountValue = voucher.DiscountValue;
                existing.StartDate = voucher.StartDate;
                existing.EndDate = voucher.EndDate;
                existing.IsActive = voucher.IsActive;
            }
            return Task.CompletedTask;
        }

        public Task DeleteVoucherAsync(string id)
        {
            var existing = _vouchers.FirstOrDefault(x => x.Id == id);
            if (existing != null) _vouchers.Remove(existing);
            return Task.CompletedTask;
        }

        // --- INVENTORY RECEIPT ---
        public Task<bool> CreateReceiptAsync(InventoryReceipt receipt)
        {
            return Task.FromResult(true);
        }
    }
}
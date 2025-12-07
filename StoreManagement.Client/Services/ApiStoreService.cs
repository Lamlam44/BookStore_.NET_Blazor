using System.Net.Http.Json;
using StoreManagement.Client.Models;

namespace StoreManagement.Client.Services
{
    public class ApiStoreService : IStoreService
    {
        private readonly HttpClient _http;

        public ApiStoreService(HttpClient http)
        {
            _http = http;
        }

        // ==========================================
        // 1. NHÀ CUNG CẤP (SUPPLIER)
        // ==========================================
        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            try
            {
                var url = "api/suppliers?PageNumber=1&PageSize=100";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Supplier>>>(url);
                return response?.Data?.Items ?? new List<Supplier>();
            }
            catch { return new List<Supplier>(); }
        }

        public async Task<Supplier?> GetSupplierByIdAsync(string id)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ApiResponse<Supplier>>($"api/suppliers/{id}");
                return response?.Data;
            }
            catch { return null; }
        }

        public async Task<Supplier?> GetSupplierWithHistoryAsync(string id) => await GetSupplierByIdAsync(id);

        public async Task<List<Supplier>> SearchSuppliersAsync(string keyword)
        {
            try
            {
                var url = $"api/suppliers/search?Query={keyword}&PageNumber=1&PageSize=100";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Supplier>>>(url);
                return response?.Data?.Items ?? new List<Supplier>();
            }
            catch { return new List<Supplier>(); }
        }

        public async Task CreateSupplierAsync(Supplier supplier)
        {
            var dto = new { supplier.SupplierName, supplier.ContactPerson, supplier.Phone, supplier.Address };
            var response = await _http.PostAsJsonAsync("api/suppliers", dto);
            if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            var dto = new { supplier.SupplierName, supplier.ContactPerson, supplier.Phone, supplier.Address };
            var response = await _http.PutAsJsonAsync($"api/suppliers/{supplier.Id}", dto);
            if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task DeleteSupplierAsync(string id) => await _http.DeleteAsync($"api/suppliers/{id}");

        // ==========================================
        // 2. KHUYẾN MÃI (VOUCHER)
        // ==========================================
        public async Task<List<Voucher>> GetVouchersAsync()
        {
            try
            {
                var url = "api/vouchers?PageNumber=1&PageSize=100";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Voucher>>>(url);
                return response?.Data?.Items ?? new List<Voucher>();
            }
            catch { return new List<Voucher>(); }
        }

        public async Task<Voucher?> GetVoucherByIdAsync(string id)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ApiResponse<Voucher>>($"api/vouchers/{id}");
                return response?.Data;
            }
            catch { return null; }
        }

        public async Task CreateVoucherAsync(Voucher v)
        {
            var dto = new { Name = v.VoucherName, Code = v.VoucherCode, DiscountValue = v.DiscountValue, StartDate = v.StartDate, EndDate = v.EndDate, IsActive = v.IsActive, Type = "PRODUCT_TARGET", TargetIds = new List<string>() };
            await _http.PostAsJsonAsync("api/vouchers", dto);
        }

        public async Task UpdateVoucherAsync(Voucher v)
        {
            var dto = new { Name = v.VoucherName, Code = v.VoucherCode, DiscountValue = v.DiscountValue, StartDate = v.StartDate, EndDate = v.EndDate, IsActive = v.IsActive, Type = "PRODUCT_TARGET", TargetIds = new List<string>() };
            await _http.PutAsJsonAsync($"api/vouchers/{v.Id}", dto);
        }

        public async Task DeleteVoucherAsync(string id) => await _http.DeleteAsync($"api/vouchers/{id}");

        // ==========================================
        // 3. SÁCH & TỒN KHO (INVENTORY & PRODUCT)
        // ==========================================
        
        public async Task<List<Book>> GetBooksAsync()
        {
            try
            {
                // Gọi API lấy danh sách sản phẩm
                var url = "api/products?PageNumber=1&PageSize=1000"; 
                
                // QUAN TRỌNG: Dùng PaginationResponse để hứng dữ liệu
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Book>>>(url);
                
                // Trả về danh sách Items bên trong
                return response?.Data?.Items ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy sách: {ex.Message}");
                return new List<Book>();
            }
        }

        public Task<Book?> GetBookByIdAsync(string id) => Task.FromResult<Book?>(null);

        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            try 
            {
                var url = $"api/products/search?Query={keyword}&PageNumber=1&PageSize=10";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Book>>>(url);
                return response?.Data?.Items ?? new List<Book>();
            }
            catch { return new List<Book>(); }
        }

        // ==========================================
        // 4. NHẬP KHO (INVENTORY RECEIPTS)
        // ==========================================
        public async Task<bool> CreateReceiptAsync(InventoryReceipt receipt)
        {
            try
            {
                var requestDto = new
                {
                    SupplierId = receipt.SupplierId,
                    Details = receipt.ReceiptDetails.Select(d => new 
                    {
                        BookId = d.BookId,
                        QuantityReceived = d.QuantityReceived, // Khớp với Backend DTO
                        UnitCost = d.UnitCost 
                    }).ToList()
                };

                var response = await _http.PostAsJsonAsync("api/inventories", requestDto);
                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Lỗi nhập kho: {await response.Content.ReadAsStringAsync()}");
                return false;
            }
            catch { return false; }
        }

        // ==========================================
        // 5. TẠO SÁCH MỚI & DROPDOWN DATA
        // ==========================================

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try {
                var response = await _http.GetFromJsonAsync<ApiResponse<List<Category>>>("api/categories");
                return response?.Data ?? new List<Category>();
            } catch { return new List<Category>(); }
        }
    }
}
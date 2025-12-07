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
        // KHU VỰC XỬ LÝ SUPPLIER (NHÀ CUNG CẤP)
        // ==========================================

        // 1. Lấy danh sách
        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            try
            {
                // Backend yêu cầu phân trang, ta truyền tham số để lấy tất cả (hoặc trang 1)
                var url = "api/suppliers?PageNumber=1&PageSize=100";
                
                // Hứng cục JSON to đùng từ Backend
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Supplier>>>(url);

                // Chỉ lấy cái list Items bên trong ra để trả về cho giao diện
                return response?.Data?.Items ?? new List<Supplier>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy danh sách Supplier: {ex.Message}");
                return new List<Supplier>();
            }
        }

        // 2. Lấy chi tiết
        public async Task<Supplier?> GetSupplierByIdAsync(string id)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ApiResponse<Supplier>>($"api/suppliers/{id}");
                return response?.Data;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        // 3. Lấy chi tiết kèm lịch sử
        public async Task<Supplier?> GetSupplierWithHistoryAsync(string id)
        {
            return await GetSupplierByIdAsync(id);
        }

        // 4. Tìm kiếm
        public async Task<List<Supplier>> SearchSuppliersAsync(string keyword)
        {
            try
            {
                var url = $"api/suppliers/search?Query={keyword}&PageNumber=1&PageSize=100";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Supplier>>>(url);
                return response?.Data?.Items ?? new List<Supplier>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tìm kiếm: " + ex.Message);
                return new List<Supplier>();
            }
        }

        // 5. Tạo mới
        public async Task CreateSupplierAsync(Supplier supplier)
        {
            var requestDto = new
            {
                SupplierName = supplier.SupplierName,
                ContactPerson = supplier.ContactPerson,
                Phone = supplier.Phone,
                Address = supplier.Address
            };

            var response = await _http.PostAsJsonAsync("api/suppliers", requestDto);
            
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi tạo mới: {error}");
            }
        }

        // 6. Cập nhật
        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            var requestDto = new
            {
                SupplierName = supplier.SupplierName,
                ContactPerson = supplier.ContactPerson,
                Phone = supplier.Phone,
                Address = supplier.Address
            };

            var response = await _http.PutAsJsonAsync($"api/suppliers/{supplier.Id}", requestDto);
            
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi cập nhật: {error}");
            }
        }
        // ==========================================
        // KHU VỰC XỬ LÝ VOUCHER (KHUYẾN MÃI)
        // ==========================================

        public async Task<List<Voucher>> GetVouchersAsync()
        {
            try
            {
                // Giả sử lấy 100 dòng đầu tiên
                var url = "api/vouchers?PageNumber=1&PageSize=100";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Voucher>>>(url);
                return response?.Data?.Items ?? new List<Voucher>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy Voucher: {ex.Message}");
                return new List<Voucher>();
            }
        }

        public async Task<Voucher?> GetVoucherByIdAsync(string id)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ApiResponse<Voucher>>($"api/vouchers/{id}");
                return response?.Data;
            }
            catch
            {
                return null;
            }
        }

        public async Task CreateVoucherAsync(Voucher voucher)
        {
            // SỬA LẠI ĐOẠN NÀY ĐỂ KHỚP DTO BACKEND
            var requestDto = new
            {
                Name = voucher.VoucherName,
                Code = voucher.VoucherCode,
                DiscountValue = voucher.DiscountValue,
                StartDate = voucher.StartDate,
                EndDate = voucher.EndDate,
                IsActive = voucher.IsActive,
                Type = "PRODUCT_TARGET",
                TargetIds = new List<string>()
            };

            var response = await _http.PostAsJsonAsync("api/vouchers", requestDto);
            
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi tạo Voucher: {error}");
            }
        }

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            var requestDto = new
            {
                Name = voucher.VoucherName,
                Code = voucher.VoucherCode,
                DiscountValue = voucher.DiscountValue,
                StartDate = voucher.StartDate,
                EndDate = voucher.EndDate,
                IsActive = voucher.IsActive,
                Type = "PRODUCT_TARGET",
                TargetIds = new List<string>()
            };

            var response = await _http.PutAsJsonAsync($"api/vouchers/{voucher.Id}", requestDto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi cập nhật: {error}");
            }
        }

        public async Task DeleteVoucherAsync(string id)
        {
            await _http.DeleteAsync($"api/vouchers/{id}");
        }
        // ==========================================
        // CÁC HÀM KHÁC (GIỮ NGUYÊN HOẶC IMPLEMENT SAU)
        // ==========================================
        public Task<List<Book>> GetBooksAsync() => Task.FromResult(new List<Book>());
        public Task<Book?> GetBookByIdAsync(string id) => Task.FromResult<Book?>(null);
        public Task<bool> CreateReceiptAsync(InventoryReceipt receipt) => Task.FromResult(true);
    }
}
using System.Net.Http.Json;
using StoreManagement.Client.Models;
using StoreManagement.Client.Models.Requests; // SỬA LẠI NAMESPACE ĐÚNG

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
                var url = "api/products?PageNumber=1&PageSize=1000"; 
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Book>>>(url);
                return response?.Data?.Items ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy sách: {ex.Message}");
                return new List<Book>();
            }
        }

        public async Task<Book?> GetBookByIdAsync(string id) 
        {
            try 
            {
                var response = await _http.GetFromJsonAsync<ApiResponse<Book>>($"api/products/{id}");
                return response?.Data;
            }
            catch { return null; }
        }

        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            try 
            {
                var url = $"api/products/search?SearchTerm={System.Net.WebUtility.UrlEncode(keyword)}&PageNumber=1&PageSize=10";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Book>>>(url);
                return response?.Data?.Items ?? new List<Book>();
            }
            catch { return new List<Book>(); }
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            var res = await _http.PostAsJsonAsync("api/products", book);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var res = await _http.PutAsJsonAsync($"api/products/{book.Id}", book);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookAsync(string id)
        {
            var res = await _http.DeleteAsync($"api/products/{id}");
            return res.IsSuccessStatusCode;
        }

        // ==========================================
        // 4. NHẬP KHO (INVENTORY RECEIPTS)
        // ==========================================
        
        // 4.1 Lấy danh sách phiếu nhập
        public async Task<List<InventoryReceipt>> GetReceiptsAsync()
        {
            try 
            {
                var url = "api/inventories/receipts?PageNumber=1&PageSize=100";
                var result = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<InventoryReceipt>>>(url);
                return result?.Data?.Items ?? new List<InventoryReceipt>();
            }
            catch { return new List<InventoryReceipt>(); }
        }

        // 4.2 Tạo phiếu nhập (DRAFT)
        public async Task<bool> CreateReceiptAsync(InventoryReceipt receipt)
        {
            try
            {
                var requestDto = new
                {
                    SupplierId = receipt.SupplierId,
                    TotalAmount = receipt.TotalCost,
                    Details = receipt.ReceiptDetails.Select(d => new 
                    {
                        BookId = d.BookId,
                        QuantityReceived = d.QuantityReceived,
                        UnitCost = d.UnitCost 
                    }).ToList()
                };
                var response = await _http.PostAsJsonAsync("api/inventories", requestDto);
                if (response.IsSuccessStatusCode) return true;
                Console.WriteLine($"Lỗi tạo phiếu: {await response.Content.ReadAsStringAsync()}");
                return false;
            }
            catch { return false; }
        }

        // 4.3 Duyệt phiếu nhập (DRAFT -> COMPLETED)
        public async Task<bool> UpdateReceiptStatusAsync(string id, string status)
        {
            try 
            {
                var request = new UpdateGRNStatusRequest { Status = status };
                // Dùng PATCH theo Controller
                var response = await _http.PatchAsJsonAsync($"api/inventories/{id}", request);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            { 
                Console.WriteLine("Lỗi Patch: " + ex.Message);
                return false; 
            }
        }
        public async Task<InventoryReceipt?> GetReceiptByIdAsync(string id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ApiResponse<InventoryReceipt>>($"api/inventories/receipts/{id}");
                return result?.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy chi tiết phiếu: {ex.Message}");
                return null;
            }
        }
        // ==========================================
        // 5. HÓA ĐƠN & BÁN HÀNG (INVOICES & POS) - BỔ SUNG LẠI
        // ==========================================
        
        public async Task<List<Invoice>> GetInvoicesAsync()
        {
            try
            {
                try 
                {
                    var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Invoice>>>("api/invoices");
                    return response?.Data?.Items ?? new List<Invoice>();
                }
                catch
                {
                    return await _http.GetFromJsonAsync<List<Invoice>>("api/invoices") ?? new List<Invoice>();
                }
            }
            catch { return new List<Invoice>(); }
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(string id)
        {
            try
            {
                try 
                {
                     var response = await _http.GetFromJsonAsync<ApiResponse<Invoice>>($"api/invoices/{id}");
                     return response?.Data;
                }
                catch 
                {
                     return await _http.GetFromJsonAsync<Invoice>($"api/invoices/{id}");
                }
            }
            catch { return null; }
        }
        
        // Alias
        public async Task<Invoice?> GetInvoiceDetailAsync(string id) => await GetInvoiceByIdAsync(id);

        public async Task<bool> CreateInvoiceAsync(CreateInvoiceRequest request)
        {
            try 
            {
                var response = await _http.PostAsJsonAsync("api/invoices", request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ==========================================
        // 6. CUSTOMERS (KHÁCH HÀNG)
        // ==========================================
        public async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                var url = "api/customers?PageNumber=1&PageSize=1000";
                var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Customer>>>(url);
                return response?.Data?.Items ?? new List<Customer>();
            }
            catch { return new List<Customer>(); }
        }
        
        public async Task<ApiResponse<PaginationResponse<Customer>>> GetCustomersAsync(int pageNumber, int pageSize)
        {
             try
            {
                var url = $"api/customers?PageNumber={pageNumber}&PageSize={pageSize}";
                return await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Customer>>>(url);
            }
            catch { return new ApiResponse<PaginationResponse<Customer>> { Success = false }; }
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            try
            {
                var dto = new { customer.Name, customer.Email, customer.Phone, customer.Address };
                var response = await _http.PostAsJsonAsync("api/customers", dto);
                if (!response.IsSuccessStatusCode) 
                    throw new Exception(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create customer: {ex.Message}");
            }
        }
        
        public async Task<ApiResponse<bool>> CreateCustomerAsync(string name, string phone, string address)
        {
             try
            {
                var dto = new { Name = name, Phone = phone, Address = address };
                var response = await _http.PostAsJsonAsync("api/customers", dto);
                if (response.IsSuccessStatusCode) return new ApiResponse<bool> { Success = true };
                return new ApiResponse<bool> { Success = false, Message = await response.Content.ReadAsStringAsync() };
            }
             catch (Exception ex) { return new ApiResponse<bool> { Success = false, Message = ex.Message }; }
        }

        // ==========================================
        // 7. CATEGORY - DANH MỤC
        // ==========================================
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try {
                 var url = "api/categories?PageNumber=1&PageSize=1000";
                 try {
                     var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Category>>>(url);
                     return response?.Data?.Items ?? new List<Category>();
                 } catch {
                      return await _http.GetFromJsonAsync<List<Category>>("api/categories") ?? new List<Category>();
                 }
            } catch { return new List<Category>(); }
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            var response = await _http.PostAsJsonAsync("api/categories", category);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var response = await _http.PutAsJsonAsync($"api/categories/{category.Id}", category);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var response = await _http.DeleteAsync($"api/categories/{id}");
            return response.IsSuccessStatusCode;
        }


        // ==========================================
        // 8. AUTHOR - TÁC GIẢ
        // ==========================================
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            try
            {
                 try {
                    var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Author>>>("api/authors?PageNumber=1&PageSize=1000");
                    return response?.Data?.Items ?? new List<Author>();
                 } catch {
                    return await _http.GetFromJsonAsync<List<Author>>("api/authors") ?? new List<Author>();
                 }
            }
            catch { return new List<Author>(); }
        }

        public async Task<bool> CreateAuthorAsync(Author author)
        {
            var res = await _http.PostAsJsonAsync("api/authors", author);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            var res = await _http.PutAsJsonAsync($"api/authors/{author.Id}", author);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAuthorAsync(string id)
        {
            var res = await _http.DeleteAsync($"api/authors/{id}");
            return res.IsSuccessStatusCode;
        }


        // ==========================================
        // 9. PUBLISHER - NHÀ XUẤT BẢN
        // ==========================================
        public async Task<List<Publisher>> GetPublishersAsync()
        {
            try
            {
                 try {
                    var response = await _http.GetFromJsonAsync<ApiResponse<PaginationResponse<Publisher>>>("api/publishers?PageNumber=1&PageSize=1000");
                    return response?.Data?.Items ?? new List<Publisher>();
                 } catch {
                    return await _http.GetFromJsonAsync<List<Publisher>>("api/publishers") ?? new List<Publisher>();
                 }
            }
            catch { return new List<Publisher>(); }
        }
        
        public async Task<List<Publisher>> GetAllPublishersAsync() => await GetPublishersAsync();

        public async Task<bool> CreatePublisherAsync(Publisher publisher)
        {
            var res = await _http.PostAsJsonAsync("api/publishers", publisher);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePublisherAsync(Publisher publisher)
        {
            var res = await _http.PutAsJsonAsync($"api/publishers/{publisher.Id}", publisher);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePublisherAsync(string id)
        {
            var res = await _http.DeleteAsync($"api/publishers/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
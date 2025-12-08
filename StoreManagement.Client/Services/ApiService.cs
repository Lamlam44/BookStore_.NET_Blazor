using Blazored.LocalStorage;
using StoreManagement.Client.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StoreManagement.Client.Services
{
    // This service acts as a placeholder for future API calls.
    // Methods are defined but not implemented, allowing the UI to be built
    // and tested with mock data, while providing a clear template for
    // connecting to a real backend later.
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";

        public ApiService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        private async Task EnsureAuthHeaderAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            if (!string.IsNullOrWhiteSpace(token))
            {
                if (_httpClient.DefaultRequestHeaders.Authorization == null || _httpClient.DefaultRequestHeaders.Authorization.Parameter != token)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        // --- Authentication ---
        // Calls /api/auth/login and returns the authentication tokens (access + refresh)
        public async Task<ApiResponse<AuthResponse>> LoginAsync(string username, string password)
        {
            try
            {
            // Backend expects { Email, Password }
            var dto = new { Email = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", dto);
                return await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>() ?? 
                    new ApiResponse<AuthResponse> { Success = false, Message = "Login failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // --- Employee Management ---
        // public async Task<ApiResponse<List<Employee>>> GetEmployeesAsync()
        // {
        //     try
        //     {
        //         var response = await _httpClient.GetFromJsonAsync<ApiResponse<PaginationResponse<Employee>>>("api/users?PageNumber=1&PageSize=1000");
        //         if (response?.Success == true)
        //         {
        //             return new ApiResponse<List<Employee>>
        //             {
        //                 Success = true,
        //                 Data = response.Data?.Items ?? new List<Employee>(),
        //                 Message = response.Message
        //             };
        //         }
        //         return new ApiResponse<List<Employee>> { Success = false, Message = response?.Message ?? "Failed to get employees" };
        //     }
        //     catch (Exception ex)
        //     {
        //         return new ApiResponse<List<Employee>> { Success = false, Message = $"Error: {ex.Message}" };
        //     }
        // }

        // public async Task<ApiResponse<Employee>> CreateEmployeeAsync(Employee employee)
        // {
        //     try
        //     {
        //         var dto = new { employee.Username, employee.Email, employee.PositionName, employee.Phone };
        //         var response = await _httpClient.PostAsJsonAsync("api/users", dto);
        //         return await response.Content.ReadFromJsonAsync<ApiResponse<Employee>>() ?? 
        //             new ApiResponse<Employee> { Success = false, Message = "Employee creation failed" };
        //     }
        //     catch (Exception ex)
        //     {
        //         return new ApiResponse<Employee> { Success = false, Message = $"Error: {ex.Message}" };
        //     }
        // }

        // public async Task<ApiResponse<Employee>> UpdateEmployeeAsync(string id, Employee employee)
        // {
        //     try
        //     {
        //         var dto = new { employee.Username, employee.Email, employee.PositionName, employee.Phone };
        //         var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", dto);
        //         return await response.Content.ReadFromJsonAsync<ApiResponse<Employee>>() ?? 
        //             new ApiResponse<Employee> { Success = false, Message = "Employee update failed" };
        //     }
        //     catch (Exception ex)
        //     {
        //         return new ApiResponse<Employee> { Success = false, Message = $"Error: {ex.Message}" };
        //     }
        // }

        // public async Task<ApiResponse> DeleteEmployeeAsync(string id)
        // {
        //     try
        //     {
        //         var response = await _httpClient.DeleteAsync($"api/users/{id}");
        //         return await response.Content.ReadFromJsonAsync<ApiResponse>() ?? 
        //             new ApiResponse { Success = false, Message = "Employee deletion failed" };
        //     }
        //     catch (Exception ex)
        //     {
        //         return new ApiResponse { Success = false, Message = $"Error: {ex.Message}" };
        //     }
        // }

        // --- Timekeeping ---
        public async Task<ApiResponse<List<ShiftHistoryEntry>>> GetShiftHistoryAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PaginationResponse<ShiftHistoryEntry>>>("api/timekeeping/history?PageNumber=1&PageSize=1000");
                if (response?.Success == true)
                {
                    return new ApiResponse<List<ShiftHistoryEntry>>
                    {
                        Success = true,
                        Data = response.Data?.Items ?? new List<ShiftHistoryEntry>(),
                        Message = response.Message
                    };
                }
                return new ApiResponse<List<ShiftHistoryEntry>> { Success = false, Message = response?.Message ?? "Failed to get shift history" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ShiftHistoryEntry>> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<bool>> StartShiftAsync()
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync("api/timekeeping/start", new { });
                return await response.Content.ReadFromJsonAsync<ApiResponse>() ?? 
                    new ApiResponse { Success = false, Message = "Start shift failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<bool>> EndShiftAsync()
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync("api/timekeeping/end", new { });
                return await response.Content.ReadFromJsonAsync<ApiResponse>() ?? 
                    new ApiResponse { Success = false, Message = "End shift failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // --- Customer Authentication ---
        public async Task<ApiResponse<AuthResponse>> CustomerLoginAsync(string email, string password)
        {
            // Reuse login endpoint - returns tokens
            return await LoginAsync(email, password);
        }

        public async Task<ApiResponse<AuthResponse>> CustomerRegisterAsync(Customer customer, string password)
        {
            try
            {
                var dto = new 
                { 
                    userName = customer.Email,
                    password = password,
                    email = customer.Email
                };
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", dto);
                return await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>() ?? 
                    new ApiResponse<AuthResponse> { Success = false, Message = "Registration failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // --- Admin: register user (uses same backend endpoint /api/auth/register)
        // This allows creating accounts by providing username, email and password.
        // Backend will assign default role (Customer) unless server exposes role assignment.
        public async Task<ApiResponse<AuthResponse>> RegisterUserAsync(string userName, string email, string password)
        {
            try
            {
                var dto = new
                {
                    userName = userName,
                    password = password,
                    email = email
                };
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", dto);
                return await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>() ??
                    new ApiResponse<AuthResponse> { Success = false, Message = "Registration failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // Create customer profile record (calls POST /api/customers)
        public async Task<ApiResponse<Customer>> CreateCustomerAsync(string name, string phone, string address)
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var dto = new { Name = name, Phone = phone, Address = address };
                var response = await _httpClient.PostAsJsonAsync("api/customers", dto);
                return await response.Content.ReadFromJsonAsync<ApiResponse<Customer>>() ?? 
                    new ApiResponse<Customer> { Success = false, Message = "Create customer failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Customer> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // Get customers (paged)
        public async Task<ApiResponse<PaginationResponse<Customer>>> GetCustomersAsync(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PaginationResponse<Customer>>>($"api/customers?PageNumber={pageNumber}&PageSize={pageSize}");
                return response ?? new ApiResponse<PaginationResponse<Customer>> { Success = false, Message = "Failed to get customers" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginationResponse<Customer>> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // --- Customer Profile ---
        public async Task<ApiResponse<UserResponse>> GetCustomerProfileAsync()
        {
            try
            {
                await EnsureAuthHeaderAsync();
                return await _httpClient.GetFromJsonAsync<ApiResponse<UserResponse>>("api/users/profile") ?? 
                    new ApiResponse<UserResponse> { Success = false, Message = "Failed to get profile" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<Customer>> UpdateCustomerProfileAsync(Customer customer)
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var dto = new 
                { 
                    email = customer.Email,
                    fullName = customer.Name,
                    phoneNumber = customer.Phone,
                    address = customer.Address,
                    dob = customer.Dob
                };
                var response = await _httpClient.PutAsJsonAsync($"api/customers/{customer.Id}", dto);
                return await response.Content.ReadFromJsonAsync<ApiResponse<Customer>>() ?? 
                    new ApiResponse<Customer> { Success = false, Message = "Update failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Customer> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse> DeleteCustomerAsync(string id)
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var response = await _httpClient.DeleteAsync($"api/customers/{id}");
                return await response.Content.ReadFromJsonAsync<ApiResponse>() ??
                    new ApiResponse { Success = false, Message = "Delete failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // --- Products ---
        public async Task<ApiResponse<List<Book>>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PaginationResponse<Book>>>("api/products?PageNumber=1&PageSize=1000");
                if (response?.Success == true)
                {
                    return new ApiResponse<List<Book>>
                    {
                        Success = true,
                        Data = response.Data?.Items ?? new List<Book>(),
                        Message = response.Message
                    };
                }
                return new ApiResponse<List<Book>> { Success = false, Message = response?.Message ?? "Failed to get products" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Book>> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<List<Book>>> SearchProductsAsync(string searchTerm, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PaginationResponse<Book>>>($"api/products/search?SearchTerm={System.Net.WebUtility.UrlEncode(searchTerm)}&PageNumber={pageNumber}&PageSize={pageSize}");
                if (response?.Success == true)
                {
                    return new ApiResponse<List<Book>>
                    {
                        Success = true,
                        Data = response.Data?.Items ?? new List<Book>(),
                        Message = response.Message
                    };
                }
                return new ApiResponse<List<Book>> { Success = false, Message = response?.Message ?? "Failed to search products" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Book>> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<Book>> GetProductByIdAsync(string id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<Book>>($"api/products/{id}") ?? 
                    new ApiResponse<Book> { Success = false, Message = "Product not found" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Book> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // --- Orders ---
        public async Task<ApiResponse<List<Invoice>>> GetOrderHistoryAsync()
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PaginationResponse<Invoice>>>("api/orders?PageNumber=1&PageSize=1000");
                if (response?.Success == true)
                {
                    return new ApiResponse<List<Invoice>>
                    {
                        Success = true,
                        Data = response.Data?.Items ?? new List<Invoice>(),
                        Message = response.Message
                    };
                }
                return new ApiResponse<List<Invoice>> { Success = false, Message = response?.Message ?? "Failed to get orders" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Invoice>> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<Invoice>> CreateOrderAsync(CreateInvoiceRequest request)
        {
            try
            {
                await EnsureAuthHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync("api/orders/online", request);
                return await response.Content.ReadFromJsonAsync<ApiResponse<Invoice>>() ?? 
                    new ApiResponse<Invoice> { Success = false, Message = "Order creation failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Invoice> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<Invoice>> GetOrderByIdAsync(string id)
        {
            try
            {
                await EnsureAuthHeaderAsync();
                return await _httpClient.GetFromJsonAsync<ApiResponse<Invoice>>($"api/orders/{id}") ??
                    new ApiResponse<Invoice> { Success = false, Message = "Order not found" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Invoice> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }
    }
}

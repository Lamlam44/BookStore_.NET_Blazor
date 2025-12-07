using StoreManagement.Client.Models;
using System.Collections.Generic;
using System.Net.Http;
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

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // --- Authentication ---
        public async Task<ApiResponse<Account>> LoginAsync(string username, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { username, password });
                return await response.Content.ReadFromJsonAsync<ApiResponse<Account>>() ?? 
                    new ApiResponse<Account> { Success = false, Message = "Login failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Account> { Success = false, Message = $"Error: {ex.Message}" };
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
        public async Task<ApiResponse<Customer>> CustomerLoginAsync(string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { username = email, password });
                return await response.Content.ReadFromJsonAsync<ApiResponse<Customer>>() ?? 
                    new ApiResponse<Customer> { Success = false, Message = "Login failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Customer> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<Customer>> CustomerRegisterAsync(Customer customer, string password)
        {
            try
            {
                var dto = new 
                { 
                    username = customer.Email, 
                    password = password,
                    email = customer.Email,
                    fullName = customer.FullName,
                    phoneNumber = customer.PhoneNumber,
                    address = customer.Address
                };
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", dto);
                return await response.Content.ReadFromJsonAsync<ApiResponse<Customer>>() ?? 
                    new ApiResponse<Customer> { Success = false, Message = "Registration failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Customer> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        // --- Customer Profile ---
        public async Task<ApiResponse<Customer>> GetCustomerProfileAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<Customer>>("api/users/profile") ?? 
                    new ApiResponse<Customer> { Success = false, Message = "Failed to get profile" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Customer> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<Customer>> UpdateCustomerProfileAsync(Customer customer)
        {
            try
            {
                var dto = new 
                { 
                    email = customer.Email,
                    fullName = customer.FullName,
                    phoneNumber = customer.PhoneNumber,
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

        public async Task<ApiResponse<Invoice>> CreateOrderAsync(Invoice invoice)
        {
            try
            {
                var dto = new
                {
                    customerId = invoice.CustomerId,
                    details = invoice.InvoiceDetails.Select(d => new
                    {
                        bookId = d.BookId,
                        quantity = d.Quantity,
                        unitPrice = d.UnitPrice
                    }).ToList()
                };
                var response = await _httpClient.PostAsJsonAsync("api/orders/online", dto);
                return await response.Content.ReadFromJsonAsync<ApiResponse<Invoice>>() ?? 
                    new ApiResponse<Invoice> { Success = false, Message = "Order creation failed" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Invoice> { Success = false, Message = $"Error: {ex.Message}" };
            }
        }
    }
}

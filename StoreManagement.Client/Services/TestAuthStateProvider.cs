using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace StoreManagement.Client.Services
{
    // Class này sẽ luôn trả về trạng thái "Đã đăng nhập là Admin"
    public class TestAuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Tạo một danh tính giả
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Admin"), // Tên hiển thị
                new Claim(ClaimTypes.Role, "ADMIN"),       // QUAN TRỌNG: Giả làm Admin
                // new Claim(ClaimTypes.Role, "Staff"),    // Mở dòng này nếu muốn test giao diện Staff
            }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
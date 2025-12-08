using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq; // Cần thêm cái này để dùng .Select nếu cần

namespace StoreManagement.Client.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _jwtHandler = new();
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        private const string TokenKey = "authToken";

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>(TokenKey);
                if (string.IsNullOrWhiteSpace(token))
                {
                    return new AuthenticationState(_anonymous);
                }

                var tokenContent = _jwtHandler.ReadJwtToken(token);

                if (tokenContent.ValidTo < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync(TokenKey);
                    return new AuthenticationState(_anonymous);
                }

                var claims = NormalizeClaims(tokenContent.Claims);
                
                // Quan trọng: Khai báo nameType và roleType để Blazor biết tìm ở đâu
                var identity = new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorage.SetItemAsync(TokenKey, token);
            var tokenContent = _jwtHandler.ReadJwtToken(token);

            var claims = NormalizeClaims(tokenContent.Claims);
            var identity = new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);
            var user = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
            var authState = Task.FromResult(new AuthenticationState(_anonymous));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> NormalizeClaims(IEnumerable<Claim> original)
        {
            var list = new List<Claim>();
            
            foreach (var c in original)
            {
                // [DEBUG QUAN TRỌNG] Dòng này sẽ in ra Console (F12) để bạn soi lỗi
                Console.WriteLine($"LOG CLAIM -> Key: {c.Type} | Value: {c.Value}");

                // 1. Xử lý Role: Bắt tất cả các kiểu viết hoa thường của role
                if (c.Type.Equals("role", StringComparison.OrdinalIgnoreCase) || 
                    c.Type.Equals("roles", StringComparison.OrdinalIgnoreCase) ||
                    c.Type.Contains("/role", StringComparison.OrdinalIgnoreCase)) // Bắt trường hợp key là URL dài
                {
                    // Luôn luôn map về ClaimTypes.Role mặc định của .NET
                    list.Add(new Claim(ClaimTypes.Role, c.Value));
                }
                // 2. Xử lý Name: Map các kiểu tên về ClaimTypes.Name
                else if (c.Type.Equals("name", StringComparison.OrdinalIgnoreCase) || 
                         c.Type.Equals("unique_name", StringComparison.OrdinalIgnoreCase) || 
                         c.Type.Equals("email", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(new Claim(ClaimTypes.Name, c.Value));
                }
                // 3. Các claim khác giữ nguyên
                else
                {
                    list.Add(c);
                }
            }
            return list;
        }
    }
}
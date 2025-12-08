using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace StoreManagement.Client.Services
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";

        public AuthorizationMessageHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            if (!string.IsNullOrWhiteSpace(token))
            {
                // ensure Authorization header is set per-request
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

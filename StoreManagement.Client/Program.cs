using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StoreManagement.Client;
using StoreManagement.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- HTTP Client ---
// Register a message handler that attaches Authorization header from local storage
builder.Services.AddScoped<AuthorizationMessageHandler>();
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>();
    // Ensure inner handler exists
    handler.InnerHandler = new HttpClientHandler();
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5254")
    };
});

// --- App Services ---
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<IStoreService, ApiStoreService>();
builder.Services.AddSingleton<CartService>();

// --- Authentication & Authorization ---
builder.Services.AddBlazoredLocalStorage();
// Use real JWT-based auth provider instead of test provider
builder.Services.AddScoped<AuthenticationStateProvider, StoreManagement.Client.Services.CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
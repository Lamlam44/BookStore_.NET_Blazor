using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StoreManagement.Client;
using StoreManagement.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri("http://localhost:5254/") 
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5254") });
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<IStoreService, ApiStoreService>(); 

await builder.Build().RunAsync();
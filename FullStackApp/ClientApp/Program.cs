using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddHttpClient("InventoryAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5214");
});

builder.Services.AddSingleton<ProductCache>();
builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();

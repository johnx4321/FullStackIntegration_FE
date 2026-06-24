using Microsoft.Extensions.Caching.Memory;
using ServerApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.MapGet("/api/productlist", (IMemoryCache cache) =>
{
    return cache.GetOrCreate("productlist", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        return ProductCatalog.Products;
    });
});

app.Run();

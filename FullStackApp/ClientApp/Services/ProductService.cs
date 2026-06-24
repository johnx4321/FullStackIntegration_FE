using System.Net.Http.Json;
using System.Text.Json;
using ClientApp.Models;

namespace ClientApp.Services;

public class ProductService(IHttpClientFactory httpClientFactory, ProductCache cache) : IProductService
{
    private readonly HttpClient _http = httpClientFactory.CreateClient("InventoryAPI");
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<Product[]?> GetProductsAsync(bool forceRefresh = false)
    {
        if (!forceRefresh && cache.Products is not null)
        {
            return cache.Products;
        }

        var products = await _http.GetFromJsonAsync<Product[]>("/api/productlist", JsonOptions);

        if (products is not null)
        {
            cache.Products = products;
        }

        return products;
    }

    public void ClearCache()
    {
        cache.Products = null;
    }
}

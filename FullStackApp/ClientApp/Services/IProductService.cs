using ClientApp.Models;

namespace ClientApp.Services;

public interface IProductService
{
    Task<Product[]?> GetProductsAsync(bool forceRefresh = false);
    void ClearCache();
}

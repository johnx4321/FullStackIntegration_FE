using ServerApp.Models;

namespace ServerApp.Data;

public static class ProductCatalog
{
    public static readonly ProductDto[] Products =
    [
        new(1, "Laptop", 1200.50, 25, new CategoryDto(101, "Electronics")),
        new(2, "Headphones", 50.00, 100, new CategoryDto(102, "Accessories"))
    ];
}

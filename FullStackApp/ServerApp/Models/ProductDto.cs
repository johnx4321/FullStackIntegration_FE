namespace ServerApp.Models;

public record CategoryDto(int Id, string Name);

public record ProductDto(int Id, string Name, double Price, int Stock, CategoryDto Category);

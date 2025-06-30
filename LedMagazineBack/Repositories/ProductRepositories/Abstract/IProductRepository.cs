using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.ProductRepositories.Abstract;

public interface IProductRepository : IBaseRepository<Product>
{
    public Task<List<Product>> GetAll(
        string? districts,
        string? screenSizes,
        decimal minPrice,
        decimal maxPrice,
        string? screenResolutions,
        bool? isActive,
        int page,
        int pageSize);
    public Task<List<Product>> GetAll();
    public Task<Product> GetById(Guid id);
}
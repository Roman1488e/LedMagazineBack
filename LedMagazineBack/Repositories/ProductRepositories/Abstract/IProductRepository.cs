using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.ProductRepositories.Abstract;

public interface IProductRepository : IBaseRepository<Product>
{
    public Task<List<Product>> GetAll();
    public Task<Product> GetById(Guid id);
    public Task<List<Product>> GetActive();
    public Task<List<Product>> GetFiltered(string? districts, string? screenSizes, decimal minPrice, decimal maxPrice, string? screenResolutions, int page, int pageSize);
}
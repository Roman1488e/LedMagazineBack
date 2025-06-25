using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services;

public interface IProductService
{
    public Task<List<Product>> GetAll();
    public Task<Product> GetById(Guid id);
    public Task<Product> Create(CreateProductModel product);
    public Task<Product> UpdateGeneralInfo(UpdateProductGenInfoModel model);
    public Task<Product> Delete(Guid id);
    public Task<Product> ChangeIsActive();
    public Task<Product> ChangePrice(decimal price);
    public Task<Product> ChangeImage(UpdateProductImageModel model);
    public Task<Product> ChangeVideo(UpdateProductVideoModel model);
    public Task<List<Product>> GetFiltered(string? districts,
        string? screenSizes,
        decimal minPrice,
        decimal maxPrice,
        string? screenResolutions,
        int page,
        int pageSize);
}
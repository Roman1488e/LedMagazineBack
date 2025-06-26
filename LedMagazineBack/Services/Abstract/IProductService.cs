using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface IProductService
{
    public Task<List<Product>> GetAll();
    public Task<Product> GetById(Guid id);
    public Task<Product> Create(CreateProductModel product);
    public Task<Product> UpdateGeneralInfo(Guid id, UpdateProductGenInfoModel model);
    public Task<Product> Delete(Guid id);
    public Task<Product> ChangeIsActive(Guid id);
    public Task<Product> ChangePrice(Guid id, UpdateProductPriceModel price);
    public Task<Product> ChangeImage(Guid id, UpdateProductImageModel model);
    public Task<Product> ChangeVideo(Guid id, UpdateProductVideoModel model);
    public Task<List<Product>> GetFiltered(string? districts,
        string? screenSizes,
        decimal minPrice,
        decimal maxPrice,
        string? screenResolutions,
        int page,
        int pageSize);
}
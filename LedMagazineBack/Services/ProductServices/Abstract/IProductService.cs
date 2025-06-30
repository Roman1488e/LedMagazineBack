using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.ProductModels.CreationModels;
using LedMagazineBack.Models.ProductModels.FiltrModels;
using LedMagazineBack.Models.ProductModels.UpdateModels;

namespace LedMagazineBack.Services.ProductServices.Abstract;

public interface IProductService
{
    public Task<List<Product>> GetAll(ProductsFilterModel filterModel);
    public Task<Product> GetById(Guid id);
    public Task<Product> Create(CreateProductModel product);
    public Task<Product> UpdateGeneralInfo(Guid id, UpdateProductGenInfoModel model);
    public Task<Product> Delete(Guid id);
    public Task<Product> ChangeIsActive(Guid id);
    public Task<Product> ChangePrice(Guid id, UpdateProductPriceModel price);
    public Task<Product> ChangeImage(Guid id, UpdateProductImageModel model);
    public Task<Product> ChangeVideo(Guid id, UpdateProductVideoModel model);
}
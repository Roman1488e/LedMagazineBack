using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class ProductService(IUnitOfWork unitOfWork, IFileService fileService) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<List<Product>> GetAll()
    {
        var products = await _unitOfWork.ProductRepository.GetAll();
        return products;
    }

    public async Task<Product> GetById(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        return product;
    }

    public async Task<Product> Create(CreateProductModel model)
    {
        var product = new Product()
        {
            BasePrice = model.BasePrice,
            Name = model.Name,
            Description = model.Description,
            Duration = model.Duration,
            IsActive = model.IsActive,
            Created = DateTime.UtcNow
        };
        if (model.Video is not null)
        {
            if(!_fileService.CheckIsVideo(model.Video))
                throw new Exception("Wrong media format");
            var videoUrl = await _fileService.UploadFile(model.Video);
            product.VideoUrl = videoUrl;
        }
        if(!_fileService.CheckIsImage(model.Image))
            throw new Exception("Wrong image format");
        var imageUrl = await _fileService.UploadFile(model.Image);
        product.ImageUrl = imageUrl;
        var result = await _unitOfWork.ProductRepository.Create(product);
        return result;
    }

    public async Task<Product> UpdateGeneralInfo(Guid id, UpdateProductGenInfoModel model)
    {
        var existingProduct = await _unitOfWork.ProductRepository.GetById(id);
        var check = false;
        if (model.Name is not null)
        {
            existingProduct.Name = model.Name;
            check = true;
        }
        if (model.Description is not null)
        {
            existingProduct.Description = model.Description;
            check = true;
        }

        if (model.Duration is not null)
        {
            existingProduct.Duration = model.Duration;
            check = true;
        }
        if (check)
            await _unitOfWork.ProductRepository.Update(existingProduct);
        return existingProduct;
    }

    public async Task<Product> Delete(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.Delete(id);
        return product;
    }

    public async Task<Product> ChangeIsActive(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        if(product.IsActive)
            product.IsActive = false;
        product.IsActive = true;
        await _unitOfWork.ProductRepository.Update(product);
        return product;
    }

    public async Task<Product> ChangePrice(Guid id, UpdateProductPriceModel price)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        product.BasePrice = price.Price;
        await _unitOfWork.ProductRepository.Update(product);
        return product;
    }

    public async Task<Product> ChangeImage(Guid id, UpdateProductImageModel model)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        if(!_fileService.CheckIsImage(model.Image))
            throw new Exception("Wrong image format");
        await _fileService.UpdateFile(product.ImageUrl, model.Image);
        return product;
    }

    public async Task<Product> ChangeVideo(Guid id, UpdateProductVideoModel model)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        if(!_fileService.CheckIsVideo(model.Video))
            throw new Exception("Wrong video format");
        if (product.VideoUrl is not null)
        {
            await _fileService.UpdateFile(product.VideoUrl, model.Video);
            return product;
        }
        var videoUrl = await _fileService.UploadFile(model.Video);
        product.VideoUrl = videoUrl;
        await _unitOfWork.ProductRepository.Update(product);
        return product;
    }

    public async Task<List<Product>> GetFiltered(string? districts, string? screenSizes, decimal minPrice, decimal maxPrice, string? screenResolutions,
        int page, int pageSize)
    {
        var products = await _unitOfWork.ProductRepository
            .GetFiltered(districts, screenSizes, minPrice, maxPrice, screenResolutions, page, pageSize);
        return products;
    }
}
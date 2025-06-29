using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.ProductModels.CreationModels;
using LedMagazineBack.Models.ProductModels.UpdateModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.ProductServices.Abstract;

namespace LedMagazineBack.Services.ProductServices;

public class ScreenSpecificationsService(IUnitOfWork unitOfWork) : IScreenSpecificationsService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ScreenSpecifications> GetById(Guid id)
    {
        var screenSpecifications = await _unitOfWork.ScreenSpecificationRepository.GetById(id);
        return screenSpecifications;
    }

    public async Task<ScreenSpecifications> GetByProductId(Guid productId)
    {
        var screenSpecifications = await _unitOfWork.ScreenSpecificationRepository.GetByProductId(productId);
        if(screenSpecifications is null)
            throw new Exception("Screen Specifications not found");
        return screenSpecifications;
    }

    public async Task<ScreenSpecifications> Create(CreateScreenSpecsModel model)
    {
        var product =  await _unitOfWork.ProductRepository.GetById(model.ProductId);
        var screenSpecifications = await _unitOfWork.ScreenSpecificationRepository.GetByProductId(product.Id);
        if (screenSpecifications is not null)
            throw new Exception("Screen specifications already exists");
        var screenSpecs = new ScreenSpecifications()
        {
            ProductId = product.Id,
            ScreenSize = model.ScreenSize,
            ScreenResolution = model.ScreenResolution,
            ScreenType = model.ScreenType
        };
        await _unitOfWork.ScreenSpecificationRepository.Create(screenSpecs);
        return screenSpecs;
    }

    public async Task<ScreenSpecifications> Delete(Guid id)
    {
        var screenSpecifications = await _unitOfWork.ScreenSpecificationRepository.Delete(id);
        return screenSpecifications;
    }

    public async Task<ScreenSpecifications> Update(Guid id, UpdateScreenSpecsModel model)
    {
        var screenSpecifications = await _unitOfWork.ScreenSpecificationRepository.GetById(id);
        var check = false;
        if (model.ScreenResolution is not null)
        {
            screenSpecifications.ScreenResolution = model.ScreenResolution;
            check = true;
        }

        if (model.ScreenSize is not null)
        {
            screenSpecifications.ScreenSize = model.ScreenSize;
            check = true;
        }

        if (model.ScreenType is not null)
        {
            screenSpecifications.ScreenType = model.ScreenType;
            check = true;
        }
        if (check)
            await _unitOfWork.ScreenSpecificationRepository.Update(screenSpecifications);
        return screenSpecifications;
    }

    public async Task<List<ScreenSpecifications>> GetAll()
    {
        var screenSpecifications = await _unitOfWork.ScreenSpecificationRepository.GetAll();
        return screenSpecifications;
    }
}
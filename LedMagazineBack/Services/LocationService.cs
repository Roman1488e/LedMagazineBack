using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class LocationService(IUnitOfWork unitOfWork) : ILocationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Location> GetById(Guid id)
    {
        var location = await _unitOfWork.LocationRepository.GetById(id);
        return location;
    }

    public async Task<List<Location>> GetAll()
    {
        var locations = await _unitOfWork.LocationRepository.GetAll();
        return locations;
    }

    public async Task<Location> GetByProductId(Guid productId)
    {
        var location = await _unitOfWork.LocationRepository.GetByProductId(productId);
        return location;
    }

    public async Task<Location> Create(CreateLocationModel model)
    {
        var location = new Location()
        {
            District = model.District,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            ProductId = model.ProductId
        };
        var result = await _unitOfWork.LocationRepository.Create(location);
        return result;
    }

    public async Task<Location> Delete(Guid id)
    {
        var location = await _unitOfWork.LocationRepository.Delete(id);
        return location;
    }

    public async Task<Location> Update(Guid id, UpdateLocationModel model)
    {
        var existingLocation = await _unitOfWork.LocationRepository.GetById(id);
        var check = false;
        if (model.District is not null)
        {
            existingLocation.District = model.District;
            check = true;
        }
        if (model.Latitude is not null)
        {
            existingLocation.Latitude = model.Latitude;
            check = true;
        }
        if (model.Longitude is not null)
        {
            existingLocation.Longitude = model.Longitude;
            check = true;
        }
        if (check)
            await _unitOfWork.LocationRepository.Update(existingLocation);
        return existingLocation;
    }
}
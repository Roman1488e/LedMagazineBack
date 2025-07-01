using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.ProductModels.CreationModels;
using LedMagazineBack.Models.ProductModels.UpdateModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.ProductServices.Abstract;

namespace LedMagazineBack.Services.ProductServices;

public class LocationService(IUnitOfWork unitOfWork, IMemoryCacheService cache) : ILocationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMemoryCacheService _cache = cache;
    private const string Key = "Locations";
    public async Task<Location> GetById(Guid id)
    {
        var cached = _cache.GetCache<List<Location>>(Key);
        if (cached is not null)
        {
            var cache = cached.SingleOrDefault(x => x.Id == id);
            if(cache is null)
                throw new Exception("Location not found");
            return cache;
        }
        await Set();
        var location = await _unitOfWork.LocationRepository.GetById(id);
        return location;
    }

    public async Task<List<Location>> GetAll()
    {
        var cached = _cache.GetCache<List<Location>>(Key);
        if(cached is not null)
            return cached;
        var locations = await _unitOfWork.LocationRepository.GetAll();
        await Set();
        return locations;
    }

    public async Task<Location> GetByProductId(Guid productId)
    {
        var cached = _cache.GetCache<List<Location>>(Key);
        if (cached is not null)
        {
            var cache = cached.SingleOrDefault(x=>x.ProductId == productId);
            if(cache is null)
                throw new Exception("Product not found");
            return cache;
        }

        await Set();
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
        await Set();
        return result;
    }

    public async Task<Location> Delete(Guid id)
    {
        var location = await _unitOfWork.LocationRepository.Delete(id);
        await Set();
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
        if (model.Latitude != 0.0d)
        {
            existingLocation.Latitude = model.Latitude;
            check = true;
        }
        if (model.Longitude != 0.0d)
        {
            existingLocation.Longitude = model.Longitude;
            check = true;
        }
        if (check)
            await _unitOfWork.LocationRepository.Update(existingLocation);
        await Set();
        return existingLocation;
    }
    
    private async Task Set()
    {
        var locations = await _unitOfWork.LocationRepository.GetAll();
        _cache.SetCache(Key,locations);
    }
}
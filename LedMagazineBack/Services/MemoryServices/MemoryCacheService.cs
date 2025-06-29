using LedMagazineBack.Services.MemoryServices.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace LedMagazineBack.Services.MemoryServices;

public class MemoryCacheService(IMemoryCache cache) : IMemoryCacheService
{
    private readonly IMemoryCache _cache = cache;

    public void SetCache<T>(string key, T value)
    {
        _cache.Set(key, value);
    }

    public T? GetCache<T>(string key)
    {
        return _cache.TryGetValue(key, out T? value) ? value : default;
    }
}
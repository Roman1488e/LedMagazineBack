namespace LedMagazineBack.Services.MemoryServices.Abstract;

public interface IMemoryCacheService
{
    public void SetCache<T>(string key, T value);
    public T? GetCache<T>(string key);
}
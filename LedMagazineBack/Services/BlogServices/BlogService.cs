using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.BlogServices.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;

namespace LedMagazineBack.Services.BlogServices;

public class BlogService(IUnitOfWork unitOfWork, IMemoryCacheService memoryCacheService) : IBlogService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMemoryCacheService _memoryCacheService = memoryCacheService;
    private const string Key = "Blog";

    public async Task<List<Blog>> GetAll()
    {
        var cached = _memoryCacheService.GetCache<List<Blog>>(Key);
        if (cached is not null)
            return cached;
        var blogs = await _unitOfWork.BlogRepository.GetAll();
        await Set();
        return blogs;
    }

    public async Task<Blog> GetById(Guid id)
    {
        var cached = _memoryCacheService.GetCache<List<Blog>>(Key);
        if (cached is not null)
        {
            var cachedBlog = cached.SingleOrDefault(b => b.Id == id);
            if(cachedBlog is null)
                throw new Exception("Blog not found");
            return cachedBlog;
        }
        await Set();
        var blog = await _unitOfWork.BlogRepository.GetById(id);
        return blog;
    }

    public async Task<Blog> Create()
    {
        var blog = await _unitOfWork.BlogRepository.Create(new Blog());
        await Set();
        return blog;
    }

    public async Task<Blog> Delete(Guid id)
    {
        var blog = await _unitOfWork.BlogRepository.Delete(id);
        await Set();
        return blog;
    }
    
    private async Task Set()
    {
        var blogs = await _unitOfWork.BlogRepository.GetAll();
        _memoryCacheService.SetCache(Key, blogs);
    } 
}
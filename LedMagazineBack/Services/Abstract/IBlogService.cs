using LedMagazineBack.Entities;

namespace LedMagazineBack.Services.Abstract;

public interface IBlogService
{
    public Task<List<Blog>> GetAll();
    public Task<Blog> GetById(Guid id);
    public Task<Blog> Create();
    public Task<Blog> Delete(Guid id);
}
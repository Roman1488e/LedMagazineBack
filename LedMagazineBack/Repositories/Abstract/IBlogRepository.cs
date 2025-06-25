using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IBlogRepository : IBaseRepository<Blog>
{
    public Task<List<Blog>> GetAll();
    public Task<Blog> GetById(Guid id);
}
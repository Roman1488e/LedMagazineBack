using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.BlogRepositories.Abstract;

public interface IBlogRepository : IBaseRepository<Blog>
{
    public Task<List<Blog>> GetAll();
    public Task<Blog> GetById(Guid id);
}
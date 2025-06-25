using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IArticleRepository : IBaseRepository<Article>
{
    public Task<List<Article>> GetAll();
    public Task<Article> GetById(Guid id);
    public Task<List<Article>> GetByTitle(string title);
    public Task<List<Article>> GetByWord(string word);
}
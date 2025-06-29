using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.BlogRepositories.Abstract;

public interface IArticleRepository : IBaseRepository<Article>
{
    public Task<List<Article>> GetAll();
    public Task<Article> GetById(Guid id);
    public Task<List<Article>> GetByTitle(string title);
    public Task<List<Article>> GetByWord(string word);
}
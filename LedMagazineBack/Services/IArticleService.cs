using LedMagazineBack.Entities;

namespace LedMagazineBack.Services;

public interface IArticleService
{
    public Task<Article> GetById(Guid id);
    public Task<List<Article>> GetAll();
    public Task<List<Article>> GetByTitle(string title);
    public Task<List<Article>> GetByWord(string word);
    public Task<Article> Delete(Guid id);
    public Task<Article> Update(Guid sessionId);
}
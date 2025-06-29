using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.BlogModels.CreationModels;
using LedMagazineBack.Models.BlogModels.UpdateModels;

namespace LedMagazineBack.Services.Abstract;

public interface IArticleService
{
    public Task<Article> GetById(Guid id);
    public Task<Article> Create(CreateArticleModel model);
    public Task<List<Article>> GetAll();
    public Task<List<Article>> GetByTitle(string title);
    public Task<List<Article>> GetByWord(string word);
    public Task<Article> Delete(Guid id);
    public Task<Article> Update(Guid id, UpdateArticleModel model);
    public Task<Article> UpdateImage(Guid id, UpdateArticleImageModel model);
    public Task<Article> UpdateVideo(Guid id, UpdateArticleVideoModel model);
}
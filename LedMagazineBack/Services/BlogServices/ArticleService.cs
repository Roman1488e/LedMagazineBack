using LedMagazineBack.Entities;
using LedMagazineBack.Models.BlogModels.CreationModels;
using LedMagazineBack.Models.BlogModels.UpdateModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.BlogServices.Abstract;
using LedMagazineBack.Services.FileServices.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace LedMagazineBack.Services.BlogServices;

public class ArticleService(IUnitOfWork unitOfWork, IFileService fileService, IMemoryCacheService memoryCache) : IArticleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;
    private readonly IMemoryCacheService _memoryCache = memoryCache;
    private const string Key = "Article";

    public async Task<Article> GetById(Guid id)
    {
        var cachedArticle = _memoryCache.GetCache<List<Article>>(Key);
        if (cachedArticle is not null)
        {
            var cache = cachedArticle.SingleOrDefault(x => x.Id == id);
            if(cache is null)
                throw new Exception("Article not found");
            return cache;
        }
        await Set();
        var article = await _unitOfWork.ArticleRepository.GetById(id);
        return article;
    }

    public async Task<Article> Create(CreateArticleModel model)
    {
        var article = new Article
        {
            Created = DateTime.UtcNow,
            Title = model.Title,
            Content = model.Content,
            BlogId = model.BlogId,
            ImageUrl = await _fileService.UploadFile(model.Image)
        };
        if(model.Video != null)
            article.VideoUrl = await _fileService.UploadFile(model.Video);
        var result = await _unitOfWork.ArticleRepository.Create(article);
        await Set();
        return result;
    }

    public async Task<List<Article>> GetAll()
    {
        var cachedArticles = _memoryCache.GetCache<List<Article>>(Key);
        if (cachedArticles is not null)
            return cachedArticles;
        await Set();
        var articles = await _unitOfWork.ArticleRepository.GetAll();
        return articles;
    }

    public async Task<List<Article>> GetByTitle(string title)
    {
        var cachedArticles = _memoryCache.GetCache<List<Article>>(Key);
        if (cachedArticles is not null)
        {
            var cache = cachedArticles.Where(x => x.Title == title)
                .ToList();
            return cache;
        }
        await Set();
        var articles = await _unitOfWork.ArticleRepository.GetByTitle(title);
        return articles;
    }

    public async Task<List<Article>> GetByWord(string word)
    {
        await Set();
        var articles = await _unitOfWork.ArticleRepository.GetByWord(word);
        return articles;
    }

    public async Task<Article> Delete(Guid id)
    {
        var article = await _unitOfWork.ArticleRepository.Delete(id);
        await Set();
        return article;
        
    }

    public async Task<Article> Update(Guid id, UpdateArticleModel model)
    {
        var exArticle = await _unitOfWork.ArticleRepository.GetById(id);
        var check = false;
        if (model.Content is not null)
        {
            exArticle.Content = model.Content;
            check = true;
        }

        if (model.Title is not null)
        {
            exArticle.Title = model.Title;
            check = true;
        }
        if (check)
            await _unitOfWork.ArticleRepository.Update(exArticle);
        await Set();
        return exArticle;
    }

    public async Task<Article> UpdateImage(Guid id, UpdateArticleImageModel model)
    {
        var exArticle = await _unitOfWork.ArticleRepository.GetById(id);
        if(!_fileService.CheckIsImage(model.Image))
            throw new InvalidOperationException("Неверный формат файла. Ожидается изображение.");
        await _fileService.UpdateFile(exArticle.ImageUrl , model.Image);
        await Set();
        return exArticle;
    }


    public async Task<Article> UpdateVideo(Guid id, UpdateArticleVideoModel model)
    {
        var exArticle = await _unitOfWork.ArticleRepository.GetById(id);
        if(!_fileService.CheckIsVideo(model.Video))
            throw new InvalidOperationException("Неверный формат файла. Ожидается видео.");
        if (exArticle.VideoUrl is not null)
        {
            await _fileService.UpdateFile(exArticle.VideoUrl, model.Video);
            return exArticle;
        }
        var filePath = await _fileService.UploadFile(model.Video);
        exArticle.VideoUrl = filePath;
        await _unitOfWork.ArticleRepository.Update(exArticle);
        await Set();
        return exArticle;
    }

    private async Task Set()
    {
        var articles = await _unitOfWork.ArticleRepository.GetAll();
        _memoryCache.SetCache(Key, articles);
    } 

}
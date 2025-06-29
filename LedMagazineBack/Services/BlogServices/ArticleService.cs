using LedMagazineBack.Entities;
using LedMagazineBack.Models.BlogModels.CreationModels;
using LedMagazineBack.Models.BlogModels.UpdateModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.BlogServices.Abstract;
using LedMagazineBack.Services.FileServices.Abstract;

namespace LedMagazineBack.Services.BlogServices;

public class ArticleService(IUnitOfWork unitOfWork, IFileService fileService) : IArticleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Article> GetById(Guid id)
    {
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
        return result;
    }

    public async Task<List<Article>> GetAll()
    {
        var articles = await _unitOfWork.ArticleRepository.GetAll();
        return articles;
    }

    public async Task<List<Article>> GetByTitle(string title)
    {
        var articles = await _unitOfWork.ArticleRepository.GetByTitle(title);
        return articles;
    }

    public async Task<List<Article>> GetByWord(string word)
    {
        var articles = await _unitOfWork.ArticleRepository.GetByWord(word);
        return articles;
    }

    public async Task<Article> Delete(Guid id)
    {
        var article = await _unitOfWork.ArticleRepository.Delete(id);
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
        return exArticle;
    }

    public async Task<Article> UpdateImage(Guid id, UpdateArticleImageModel model)
    {
        var exArticle = await _unitOfWork.ArticleRepository.GetById(id);
        if(!_fileService.CheckIsImage(model.Image))
            throw new InvalidOperationException("Неверный формат файла. Ожидается изображение.");
        await _fileService.UpdateFile(exArticle.ImageUrl , model.Image);
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
        return exArticle;
    }

}
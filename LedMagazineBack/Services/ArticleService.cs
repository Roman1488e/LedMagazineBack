using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class ArticleService(IUnitOfWork unitOfWork, IFileService fileService) : IArticleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;

    public async Task<Article> GetById(Guid id)
    {
        var article = await _unitOfWork.ArticleRepository.GetById(id);
        return article;
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
        var filePath = await _fileService.UploadFile(model.Image);
        exArticle.ImageUrl = filePath;
        await _unitOfWork.ArticleRepository.Update(exArticle);
        return exArticle;
    }


    public async Task<Article> UpdateVideo(Guid id, UpdateArticleVideoModel model)
    {
        var exArticle = await _unitOfWork.ArticleRepository.GetById(id);
        if(!_fileService.CheckIsVideo(model.Video))
            throw new InvalidOperationException("Неверный формат файла. Ожидается видео.");
        var filePath = await _fileService.UploadFile(model.Video);
        exArticle.VideoUrl = filePath;
        await _unitOfWork.ArticleRepository.Update(exArticle);
        return exArticle;
    }

}
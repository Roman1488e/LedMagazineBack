using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories;

public class ArticleRepository(MagazineDbContext context) : IArticleRepository
{
    private readonly MagazineDbContext  _context = context;

    public async Task<Article> Create(Article entity)
    {
        await _context.Articles.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Article> Update(Article entity)
    {
        _context.Articles.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Article> Delete(Guid id)
    {
        var article = await _context.Articles.SingleOrDefaultAsync(x=>x.Id == id);
        if(article is null)
            throw new Exception("Article not found");
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<List<Article>> GetAll()
    {
        var articles = await _context.Articles.AsNoTracking().ToListAsync();
        return articles;
    }

    public async Task<Article> GetById(Guid id)
    {
        var article = await _context.Articles.SingleOrDefaultAsync(x => x.Id == id);
        if(article is null)
            throw new Exception("Article not found");
        return article;
    }

    public async Task<List<Article>> GetByTitle(string title)
    {
        var articles = await _context.Articles.AsNoTracking().Where(x => x.Title == title).ToListAsync();
        return articles;
    }

    public async Task<List<Article>> GetByWord(string word)
    {
        var articles = await _context.Articles.AsNoTracking()
            .Where(x => x.Title.Contains(word) || x.Content.Contains(word)).ToListAsync();
        return articles;
    }
}
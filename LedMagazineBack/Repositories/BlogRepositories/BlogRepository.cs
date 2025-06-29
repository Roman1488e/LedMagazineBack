using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BlogRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.BlogRepositories;

public class BlogRepository(MagazineDbContext context) : IBlogRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<Blog> Create(Blog entity)
    {
        await _context.Blogs.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Blog> Update(Blog entity)
    {
        _context.Blogs.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Blog> Delete(Guid id)
    {
        var blog = await _context.Blogs.SingleOrDefaultAsync(x=> x.Id == id);
        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return blog;
    }

    public async Task<List<Blog>> GetAll()
    {
        var blogs = await _context.Blogs.AsNoTracking().Include(x=> x.Articles).ToListAsync();
        return blogs;
    }

    public async Task<Blog> GetById(Guid id)
    {
        var blog = await _context.Blogs.Include(X=> X.Articles).SingleOrDefaultAsync(x => x.Id == id);
        if (blog is null)
            throw new Exception("Blog not found");
        return blog;
    }
}
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class BlogService(IUnitOfWork unitOfWork) : IBlogService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<Blog>> GetAll()
    {
        var blogs = await _unitOfWork.BlogRepository.GetAll();
        return blogs;
    }

    public async Task<Blog> GetById(Guid id)
    {
        var blog = await _unitOfWork.BlogRepository.GetById(id);
        return blog;
    }

    public async Task<Blog> Create()
    {
        var blog = await _unitOfWork.BlogRepository.Create(new Blog());
        return blog;
    }

    public async Task<Blog> Delete(Guid id)
    {
        var blog = await _unitOfWork.BlogRepository.Delete(id);
        return blog;
    }
}
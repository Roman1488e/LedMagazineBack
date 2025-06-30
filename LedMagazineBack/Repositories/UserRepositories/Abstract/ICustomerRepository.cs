using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.UserRepositories.Abstract;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    public Task<List<Customer>> GetAll(
        string? role,
        string? organisationName,
        string? anyWord,
        bool? isVerified,
        int page,
        int pageSize);
    public Task<List<Customer>> GetAll();
    public Task<Customer?> GetByUsername(string username);
    public Task<Customer> GetById(Guid id);
}
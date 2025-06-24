using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    public Task<List<Customer>> GetAll();
    public Task<Customer> GetById(Guid id);
    public Task<List<Customer>> GetAllByRole(string role);
    public Task<Customer> GetAllByUsername(string userName);
    public Task<List<Customer>> GetByAnyWord(string word);
}
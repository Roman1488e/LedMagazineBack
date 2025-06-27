using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    public Task<List<Customer>> GetAll();
    public Task<Customer> GetById(Guid id);
    public Task<List<Customer>> GetAllByRole(string role);
    public Task<List<Customer>> GetVerified();
    public Task<List<Customer>> GetByOrgName(string role);
    public Task<Customer?> GetByUsername(string userName);
    public Task<List<Customer>> GetByAnyWord(string word);
}
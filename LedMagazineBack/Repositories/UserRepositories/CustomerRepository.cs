using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.UserRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.UserRepositories;

public class CustomerRepository(MagazineDbContext context) : ICustomerRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<Customer> Create(Customer entity)
    {
        await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Customer> Update(Customer entity)
    {
        _context.Customers.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Customer> Delete(Guid id)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
        if(customer is null)
            throw new Exception("Customer not found");
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<List<Customer>> GetAll()
    {
        var customers = await _context.Customers.AsNoTracking()
            .Include(x=> x.Orders).Include(x=> x.Cart).ToListAsync();
        return customers;
    }

    public async Task<Customer> GetById(Guid id)
    {
        var customer = await _context.Customers
            .Include(x=> x.Orders).Include(x=> x.Cart).SingleOrDefaultAsync(x => x.Id == id);
        if(customer is null)
            throw new Exception("Customer not found");
        return customer;
    }

    public async Task<List<Customer>> GetAllByRole(string role)
    {
        var customers = await _context.Customers.AsNoTracking()
            .Include(x=> x.Orders).Include(x=> x.Cart)
            .Where(x=> x.Role == role).ToListAsync();
        return customers;
    }
    

    public async Task<List<Customer>> GetVerified()
    {
        var customers = await _context.Customers.AsNoTracking()
            .Include(x=> x.Orders).Include(x=> x.Cart)
            .Where(x => x.IsVerified).ToListAsync();
        return customers;
    }

    public async Task<List<Customer>> GetByOrgName(string orgName)
    {
        var customers = await _context.Customers.AsNoTracking()
            .Include(x=> x.Orders).Include(x=> x.Cart)
            .Where(x=> x.OrganisationName == orgName).ToListAsync();
        return customers;
    }

    public async Task<Customer?> GetByUsername(string userName)
    {
        var customer = await _context.Customers.Include(x=> x.Orders).Include(x=> x.Cart)
            .SingleOrDefaultAsync(x => x.Username == userName);
        return customer;
    }

    public async Task<List<Customer>> GetByAnyWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return new List<Customer>();

        word = word.ToLower().Trim();

        var customers = await _context.Customers
            .AsNoTracking()
            .Include(x => x.Orders)
            .Include(x => x.Cart)
            .Where(x =>
                x.Name.ToLower().Contains(word) ||
                x.Role.ToLower().Contains(word) ||
                (x.ContactNumber != null && x.ContactNumber.ToLower().Contains(word)) ||
                x.Username.ToLower().Contains(word))
            .ToListAsync();

        return customers;
    }

}
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

    

    public async Task<Customer> GetById(Guid id)
    {
        var customer = await _context.Customers
            .Include(x=> x.Orders).Include(x=> x.Cart).SingleOrDefaultAsync(x => x.Id == id);
        if(customer is null)
            throw new Exception("Customer not found");
        return customer;
    }

    public async Task<List<Customer>> GetAll(
        string? role = null,
        string? organisationName = null,
        string? anyWord = null,
        bool? isVerified = null,
        int page = 1,
        int pageSize = 10)
    {
        var query = _context.Customers
            .AsNoTracking()
            .Include(x => x.Orders)
            .Include(x => x.Cart)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(role))
            query = query.Where(x => x.Role == role.ToLower());

        if (!string.IsNullOrWhiteSpace(organisationName))
            query = query.Where(x => x.OrganisationName.ToLower() == organisationName.ToLower());

        if (isVerified.HasValue)
            query = query.Where(x => x.IsVerified == isVerified.Value);

        if (!string.IsNullOrWhiteSpace(anyWord))
        {
            var word = anyWord.Trim().ToLower();
            query = query.Where(x =>
                x.Name.ToLower().Contains(word) ||
                x.Role.ToLower().Contains(word) ||
                (x.ContactNumber != null && x.ContactNumber.ToLower().Contains(word)));
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Customer>> GetAll()
    {
        var customers = await _context.Customers
            .Include(x => x.Orders).Include(x => x.Cart).ToListAsync();
        return customers;
    }

    public async Task<Customer?> GetByUsername(string userName)
    {
        return await _context.Customers
            .Include(x => x.Orders)
            .Include(x => x.Cart)
            .SingleOrDefaultAsync(x => x.Username == userName.ToLower());
    }




}
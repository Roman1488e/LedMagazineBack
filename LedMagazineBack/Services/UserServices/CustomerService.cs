using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models;
using LedMagazineBack.Models.ProductModels.UpdateModels;
using LedMagazineBack.Models.UserModels.Auth;
using LedMagazineBack.Models.UserModels.FilterModels;
using LedMagazineBack.Models.UserModels.UpdateModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.UserServices.Abstract;
using Microsoft.AspNetCore.Identity;

namespace LedMagazineBack.Services.UserServices;

public class CustomerService(IJwtService jwtService, IUnitOfWork unitOfWork, UserHelper userHelper, IMemoryCacheService cache) : ICustomerService
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly UserHelper _userHelper = userHelper;
    
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMemoryCacheService _cache = cache;
    private const string Key = "customers";
    private readonly RolesConstants  _rolesConstants = new RolesConstants();

    public async Task<List<Customer>> GetAll(FilterCustomersModel model)
    {
        await Set();
        if(model.page == 0)
            model.page = 1;
        if(model.pageSize == 0)
            model.pageSize = 10;
        var customers = await _unitOfWork.CustomerRepository.GetAll(
           model.role,
           model.organisationName,
           model.anyWord,
           model.isVerified,
           model.page,
           model.pageSize
            );
        return customers;
    }

    public async Task<Customer> GetById(Guid id)
    {
        var cachedCustomers = _cache.GetCache<List<Customer>>(Key);
        if (cachedCustomers is not null && cachedCustomers.Count != 0)
        {
            var cachedCustomer = cachedCustomers.SingleOrDefault(x => x.Id == id);
            if (cachedCustomer is null)
                throw new Exception("Customer not found");
            return cachedCustomer;
        }
        await Set();
        var customer = await _unitOfWork.CustomerRepository.GetById(id);
        return customer;
    }

    public async Task<Customer> GetByUsername(string username)
    {
        var cachedCustomers = _cache.GetCache<List<Customer>>(Key);
        if (cachedCustomers is not null  && cachedCustomers.Count != 0)
        {
            var cachedCustomer = cachedCustomers.SingleOrDefault(x => x.Username == username);
            if (cachedCustomer is null)
                throw new Exception("Customer not found");
            return cachedCustomer;
        }
        await Set();
        var customer = await _unitOfWork.CustomerRepository.GetByUsername(username);
        if(customer == null)
            throw new Exception("Customer not found");
        return customer;
    }

    public async Task<Customer> UpdateGenInfo(UpdateClientGenInfModel model)
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(_userHelper.GetUserId());
        customer.Name =  model.Name;
        await Set();
        await _unitOfWork.CustomerRepository.Update(customer);
        return customer;
    }

    public async Task<Customer> UpdateContactNumber(UpdateContactNumberModel model)
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(_userHelper.GetUserId());
        if(model.ContactNumber is null)
            return customer;
        customer.ContactNumber = model.ContactNumber;
        await Set();
        await _unitOfWork.CustomerRepository.Update(customer);
        return customer;
    }

    public async Task<Customer> ChangeVerify(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(id);
        if(customer.IsVerified)
            customer.IsVerified = false;
        customer.IsVerified = true;
        await Set();
        await _unitOfWork.CustomerRepository.Update(customer);
        return customer;
    }

    public async Task<Customer> UpdateUsername(UpdateUsernameModel model)
    {
        var exCustomer = await _unitOfWork.CustomerRepository.GetById(_userHelper.GetUserId());
        if (model.Username is null)
            return exCustomer;
        var customer = await _unitOfWork.CustomerRepository.GetByUsername(model.Username);
        if(customer is not null)
            throw new Exception("Username already exists");
        exCustomer.Username = model.Username.ToLower();
        await Set();
        await _unitOfWork.CustomerRepository.Update(exCustomer);
        return exCustomer;
    }

    public async Task<Customer> UpdateRole(Guid id, UpdateRoleModel model)
    {
        var exCustomer = await _unitOfWork.CustomerRepository.GetById(id);
        if(model.Role is null)
            return exCustomer;
        var role = model.Role.ToLower();
        if(role != _rolesConstants.Admin && role != _rolesConstants.Guest && role != _rolesConstants.Customer)
            throw new Exception("Invalid role");
        exCustomer.Role = role;
        await Set();
        await _unitOfWork.CustomerRepository.Update(exCustomer);
        return exCustomer;
    }

    public async Task<Customer> UpdatePassword(UpdatePasswordModel model)
    {
        var exCustomer = await _unitOfWork.CustomerRepository.GetById(_userHelper.GetUserId());
        var oldPasswordHash = new PasswordHasher<Customer>().VerifyHashedPassword(exCustomer, exCustomer.PasswordHash, model.OldPassword);
        if(oldPasswordHash == PasswordVerificationResult.Failed)
            throw new Exception("Invalid old password");
        if(model.NewPassword == model.OldPassword)
            return exCustomer;
        if(model.NewPassword != model.ConfirmNewPassword)
            throw new Exception("Invalid confirm password");
        exCustomer.PasswordHash = new PasswordHasher<Customer>().HashPassword(exCustomer, model.NewPassword);
        await _unitOfWork.CustomerRepository.Update(exCustomer);
        await Set();
        return exCustomer;
    }

    public async Task<Customer> Delete(Guid? id)
    {
        id ??= _userHelper.GetUserId();
        await Set();
        var customer = await _unitOfWork.CustomerRepository.Delete(id.Value);
        return customer;
    }
    

    public async Task<string> Login(LoginModel model)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByUsername(model.Username);
        if(customer is null)
            throw new Exception("Wrong username or password");
        var hashVerification =  new PasswordHasher<Customer>().VerifyHashedPassword(customer, customer.PasswordHash, model.Password);
        if(hashVerification == PasswordVerificationResult.Failed)
            throw new Exception("Wrong username or password");
        if (string.IsNullOrEmpty(customer.Role))
        {
            customer.Role = _rolesConstants.Customer;
            await _unitOfWork.CustomerRepository.Update(customer);
        }
        var cart = await _unitOfWork.CartRepository.GetByCustomerId(customer.Id);
        if(cart is null)
            throw new Exception("Cart not found");
        if(cart.Items.Count == 0)
        {
            cart = await _unitOfWork.CartRepository.GetBySessionId(_userHelper.GetUserId());
            if (cart is null)
                throw new Exception("Cart not found");
            if (cart.Items.Count == 0 && cart.CustomerId is null)
            {
                var token1 = _jwtService.GenerateTokenForCustomer(customer);
                return token1;
            }
            cart.CustomerId = customer.Id;
            cart.SessionId = null;
            await _unitOfWork.CartRepository.Update(cart);
        }

        await Set();
        var token = _jwtService.GenerateTokenForCustomer(customer);
        return token;

    }

    public async Task<Customer> Register(RegisterModel model)
    {
        var cart = await _unitOfWork.CartRepository.GetBySessionId(_userHelper.GetUserId());
        if(cart is null)
            throw new Exception("Cart not found");
        var exCustomer = await _unitOfWork.CustomerRepository.GetByUsername(model.Username);
        if(exCustomer is not null)
            throw new Exception("Username already exists");
        var customer = new Customer()
        {
            IsVerified = false,
            Name = model.Name,
            Role = _rolesConstants.Customer,
            Username = model.Username.ToLower(),
            OrganisationName = model.OrganisationName,
            ContactNumber = model.ContactNumber,
        };
        customer.PasswordHash = new PasswordHasher<Customer>().HashPassword(customer, model.Password);
        var result = await _unitOfWork.CustomerRepository.Create(customer);
        cart.CustomerId = result.Id;
        await _unitOfWork.CartRepository.Update(cart);
        await Set();
        return result;
    }
    
    private async Task Set()
    {
        var customers = await _unitOfWork.CustomerRepository.GetAll();
        _cache.SetCache(Key,customers);
    }
    
}
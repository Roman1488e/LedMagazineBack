using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace LedMagazineBack.Services;

public class CustomerService(IJwtService jwtService, IUnitOfWork unitOfWork, UserHelper userHelper) : ICustomerService
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly UserHelper _userHelper = userHelper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RolesConstants  _rolesConstants = new RolesConstants();

    public async Task<List<Customer>> GetAll()
    {
        var customers = await _unitOfWork.CustomerRepository.GetAll();
        return customers;
    }

    public async Task<Customer> GetById(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(id);
        return customer;
    }

    public async Task<Customer> GetByUsername(string username)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByUsername(username);
        if(customer == null)
            throw new Exception("Customer not found");
        return customer;
    }

    public async Task<Customer> UpdateGenInfo(Guid id, UpdateClientGenInfModel model)
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(id);
        customer.Name =  model.Name;
        await _unitOfWork.CustomerRepository.Update(customer);
        return customer;
    }

    public async Task<Customer> UpdateContactNumber(Guid id, UpdateContactNumberModel model)
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(id);
        if(model.ContactNumber is null)
            return customer;
        customer.ContactNumber = model.ContactNumber;
        await _unitOfWork.CustomerRepository.Update(customer);
        return customer;
    }

    public async Task<Customer> ChangeVerify(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(id);
        if(customer.IsVerified)
            customer.IsVerified = false;
        customer.IsVerified = true;
        await _unitOfWork.CustomerRepository.Update(customer);
        return customer;
    }

    public async Task<Customer> UpdateUsername(Guid id, UpdateUsernameModel model)
    {
        var exCustomer = await _unitOfWork.CustomerRepository.GetById(id);
        if (model.Username is null)
            return exCustomer;
        var customer = await _unitOfWork.CustomerRepository.GetByUsername(model.Username);
        if(customer is not null)
            throw new Exception("Username already exists");
        exCustomer.Username = model.Username;
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
        await _unitOfWork.CustomerRepository.Update(exCustomer);
        return exCustomer;
    }

    public async Task<Customer> UpdatePassword(Guid id, UpdatePasswordModel model)
    {
        var exCustomer = await _unitOfWork.CustomerRepository.GetById(id);
        var oldPasswordHash = new PasswordHasher<Customer>().VerifyHashedPassword(exCustomer, model.OldPassword, exCustomer.PasswordHash);
        if(oldPasswordHash == PasswordVerificationResult.Failed)
            throw new Exception("Invalid old password");
        if(model.NewPassword == model.OldPassword)
            return exCustomer;
        if(model.NewPassword != model.ConfirmNewPassword)
            throw new Exception("Invalid confirm password");
        exCustomer.PasswordHash = new PasswordHasher<Customer>().HashPassword(exCustomer, model.NewPassword);
        await _unitOfWork.CustomerRepository.Update(exCustomer);
        return exCustomer;
    }

    public async Task<Customer> Delete(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.Delete(id);
        return customer;
    }

    public async Task<List<Customer>> GetAllUsers()
    {
        var customers = await _unitOfWork.CustomerRepository.GetAllByRole(_rolesConstants.Customer);
        return customers;
    }

    public async Task<List<Customer>> GetAllAdmins()
    {
        var customers = await _unitOfWork.CustomerRepository.GetAllByRole(_rolesConstants.Admin);
        return customers;
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
        if (cart.Items.Count == 0)
        {
            cart = await _unitOfWork.CartRepository.GetBySessionId(_userHelper.GetUserId());
            if(cart is null)
                throw new Exception("Cart not found");
            await _unitOfWork.CartRepository.Update(cart);
        }
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
        if(model.Password != model.ConfirmPassword)
            throw new Exception("Password doesn't match");
        var customer = new Customer()
        {
            IsVerified = false,
            Name = model.Name,
            Role = _rolesConstants.Customer,
            Username = model.Username.ToLower(),
            OrganisationName = model.OrganisationName,
        };
        customer.PasswordHash = new PasswordHasher<Customer>().HashPassword(customer, model.Password);
        if(model.ContactNumber is not null)
            customer.ContactNumber = model.ContactNumber;
        var result = await _unitOfWork.CustomerRepository.Create(customer);
        if (cart.Items.Count == 0) return result;
        cart.CustomerId = result.Id;
        cart.SessionId = null;
        await _unitOfWork.CartRepository.Update(cart);
        return result;
    }
}
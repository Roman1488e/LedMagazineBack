using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface ICustomerService
{
    public Task<List<Customer>> GetAll();
    public Task<Customer> GetById(Guid id);
    public Task<Customer> GetByUsername(string username);
    public Task<Customer> UpdateGenInfo(Guid id, UpdateClientGenInfModel model);
    public Task<Customer> UpdateContactNumber(Guid id, UpdateContactNumberModel model);
    public Task<Customer> ChangeVerify(Guid id);
    public Task<Customer> UpdateUsername(Guid id, UpdateLocationModel model);
    public Task<Customer> UpdateRole(Guid id, UpdateRoleModel role);
    public Task<Customer> UpdatePassword(Guid id, UpdatePasswordModel password);
    public Task<Customer> Delete(Guid id);
    public Task<List<Customer>> GetAllUsers();
    public Task<List<Customer>> GetAllAdmins();
    public Task<string> Login(LoginModel model);
    public Task<Customer> Register(RegisterModel model);
}
using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services;

public interface ICustomerService
{
    public Task<List<Customer>> GetAll();
    public Task<Customer> GetById(Guid id);
    public Task<Customer> GetByUsername(string username);
    public Task<Customer> UpdateGenInfo(UpdateClientGenInfModel model);
    public Task<Customer> UpdateContactNumber(UpdateContactNumberModel model);
    public Task<Customer> UpdateUsername(UpdateLocationModel model);
    public Task<Customer> UpdateRole(UpdateRoleModel role);
    public Task<Customer> UpdatePassword(UpdatePasswordModel password);
    public Task<Customer> Delete(Guid id);
    public Task<List<Customer>> GetAllUsers();
    public Task<List<Customer>> GetAllAdmins();
}
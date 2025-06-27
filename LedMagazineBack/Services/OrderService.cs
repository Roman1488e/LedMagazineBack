using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class OrderService(IUnitOfWork unitOfWork, UserHelper userHelper) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserHelper  _userHelper = userHelper;
    private readonly RolesConstants  _rolesConstants = new RolesConstants();

    public async Task<List<Order>> GetAll()
    {
        var orders = await _unitOfWork.OrderRepository.GetAll();
        return orders;
    }

    public async Task<Order> GetById(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetById(id);
        return order;
    }

    public async Task<Order> Create(CreateOrderModel model)
    {
        var sessionId = _userHelper.GetUserId();
        var order = new Order()
        {
            Created = DateTime.UtcNow,
            IsAccepted = false,
            IsActive = true,
            IsPrimary = false,
            OrganisationName = model.OrganisationName,
            PhoneNumber = model.PhoneNumber,
            SessionId = sessionId,
        };
        var result = await _unitOfWork.OrderRepository.Create(order);
        return result;
    }

    public async Task<Order> Create()
    {
        var customer = await _unitOfWork.CustomerRepository.GetById(_userHelper.GetUserId());
        var order = new Order()
        {
            Created = DateTime.UtcNow,
            IsAccepted = false,
            IsActive = true,
            IsPrimary = customer.IsVerified,
            OrganisationName = customer.OrganisationName,
            PhoneNumber = customer.ContactNumber,
            CustomerId = customer.Id,
        };
        var result = await _unitOfWork.OrderRepository.Create(order);
        return result;
    }

    public async Task<Order> Accept(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetById(id);
        if (!order.IsAccepted)
            order.IsAccepted = true;
        await _unitOfWork.OrderRepository.Update(order);
        return order;
    }

    public async Task<Order> Cancel(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetById(id);
        if (!order.IsActive)
            order.IsActive = false;
        await _unitOfWork.OrderRepository.Update(order);
        return order;
    }

    public async Task<Order> Delete(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.Delete(id);
        return order;
    }

    public async Task<Order> Update(Guid id, UpdateOrderModel model)
    {
        var order = await _unitOfWork.OrderRepository.GetById(id);
        var check = false;
        if (model.OrganisationName is not null)
        {
            order.OrganisationName = model.OrganisationName;
            check = true;
        }

        if (model.PhoneNumber is not null)
        {
            order.PhoneNumber = model.PhoneNumber;
            check = true;
        }
        if (check)
            await _unitOfWork.OrderRepository.Update(order);
        return order;
    }
}
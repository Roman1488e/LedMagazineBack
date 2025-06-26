using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
        var order = new Order()
        {
            Created = DateTime.UtcNow, //customerId
            IsAccepted = false,
            IsActive = true,
            OrganisationName = model.OrganisationName,
            PhoneNumber = model.PhoneNumber
        };
        var result = await _unitOfWork.OrderRepository.Create(order);
        return result;
    }

    public async Task<Order> Create()
    {
        throw new NotImplementedException();
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
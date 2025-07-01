using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models.OrderModels.CreationModels;
using LedMagazineBack.Models.OrderModels.FilterModel;
using LedMagazineBack.Models.OrderModels.UpdateModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.OrderServices.Abstract;
using LedMagazineBack.Services.TelegramServices.Abstract;

namespace LedMagazineBack.Services.OrderServices;

public class OrderService(IUnitOfWork unitOfWork, UserHelper userHelper, IServiceScopeFactory scopeFactory) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly UserHelper  _userHelper = userHelper;
    private readonly RolesConstants  _rolesConstants = new RolesConstants();

    public async Task<List<Order>> GetAll(FilterOrdersModel model)
    {
        if(model.page == 0)
            model.page = 1;
        if(model.pageSize == 0)
            model.pageSize = 10;
        var orders = await _unitOfWork.OrderRepository.GetAll(
            model.productName, model.orgName,
            model.isAccepted, model.isActive,
            model.isPrimary, model.startDate, model.endDate
            , model.minPrice, model.maxPrice, model.page, model.pageSize);
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

    public async Task<Order> CreateFromCart(CreateOrderModel model)
    {
        var cart = await _unitOfWork.CartRepository.GetBySessionId(_userHelper.GetUserId());
        if (cart is null || cart.Items.Count == 0)
            throw new Exception("Cart is empty");

        var order = new Order()
        {
            Created = DateTime.UtcNow,
            IsAccepted = false,
            IsActive = true,
            IsPrimary = false,
            OrganisationName = model.OrganisationName,
            PhoneNumber = model.PhoneNumber,
            SessionId = _userHelper.GetUserId(),
            TotalPrice = cart.TotalPrice
        };

        var createdOrder = await _unitOfWork.OrderRepository.Create(order);

        foreach (var item in cart.Items)
        {
            var orderItem = new OrderItem()
            {
                OrderId = createdOrder.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
            };

            var createdOrderItem = await _unitOfWork.OrderItemRepository.Create(orderItem);

            item.RentTime.CartItemId = null;
            item.RentTime.OrderItemId = createdOrderItem.Id;
            await _unitOfWork.RentTimeRepository.Update(item.RentTime);
        }
        
        await _unitOfWork.CartItemRepository.DeleteRange(cart.Items);
        cart.TotalPrice = 0;
        await _unitOfWork.CartRepository.Update(cart);
        _ = Task.Run((Func<Task>)(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramService>();
            try
            {
                await telegramService.GenerateMessageAsync(order.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка отправки уведомления Telegram: {ex.Message}");
            }
        }));
        return createdOrder;
    }


    public async Task<Order> CreateFromCart()
    {
        var cart = await _unitOfWork.CartRepository.GetByCustomerId(_userHelper.GetUserId());
        var customer = await _unitOfWork.CustomerRepository.GetById(_userHelper.GetUserId());
        if(cart is null || cart.Items.Count == 0)
            throw new Exception("Cart is empty");
        var order = new Order()
        {
            Created = DateTime.UtcNow,
            IsAccepted = false,
            IsActive = true,
            IsPrimary = false,
            OrganisationName = customer.OrganisationName,
            PhoneNumber = customer.ContactNumber,
            CustomerId = _userHelper.GetUserId(),
            TotalPrice = cart.TotalPrice
        };
        var createdOrder = await _unitOfWork.OrderRepository.Create(order);
        foreach (var item in cart.Items)
        {
            var orderItem = new OrderItem()
            {
                OrderId = createdOrder.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
            };
            var createdOrderItem = await _unitOfWork.OrderItemRepository.Create(orderItem);
            var rentTime = item.RentTime;
            rentTime.CartItemId = null;
            rentTime.OrderItemId = createdOrderItem.Id;
            await _unitOfWork.RentTimeRepository.Update(rentTime);
        }
        await _unitOfWork.CartItemRepository.DeleteRange(cart.Items);
        _ = Task.Run((Func<Task>)(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramService>();
            try
            {
                await telegramService.GenerateMessageAsync(order.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка отправки уведомления Telegram: {ex.Message}");
            }
        }));
        cart.TotalPrice = 0;
        await _unitOfWork.CartRepository.Update(cart);
        return createdOrder;
    }

    public async Task<Order?> GetByOrderNumber(uint orderNumber)
    {
        var order = await _unitOfWork.OrderRepository.GetByOrderNumber(orderNumber);
        return order;
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
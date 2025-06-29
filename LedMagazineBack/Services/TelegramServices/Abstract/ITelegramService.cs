namespace LedMagazineBack.Services.TelegramServices.Abstract;

public interface ITelegramService
{
    public Task SendMessageAsync(string message);
    
    public Task GenerateMessageAsync(Guid orderId);
}
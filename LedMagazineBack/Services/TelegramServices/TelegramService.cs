using LedMagazineBack.Models.TelegramModel;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.TelegramServices.Abstract;
using Microsoft.Extensions.Options;

namespace LedMagazineBack.Services.TelegramServices;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class TelegramService(HttpClient httpClient, IOptions<TelegramSettings> options, IUnitOfWork unitOfWork) : ITelegramService
{
    private readonly TelegramSettings _settings = options.Value;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task SendMessageAsync(string message)
    {
        if (string.IsNullOrEmpty(_settings.BotToken) || _settings.ChatIds.Count == 0)
            return;

        foreach (var chatId in _settings.ChatIds)
        {
            var url = $"https://api.telegram.org/bot{_settings.BotToken}/sendMessage";

            var payload = new
            {
                chat_id = chatId,
                text = message,
                parse_mode = "HTML"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode) continue;
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"❌ Ошибка отправки Telegram-сообщения: {error}");
        }
    }
    
    public async Task GenerateMessageAsync(Guid orderId)
    {
        var order = await _unitOfWork.OrderRepository.GetById(orderId);

        var message = new StringBuilder();
        message.AppendLine($"📦 Новый заказ №{order.OrderNumber}");
        message.AppendLine($"🆔 ID заказа: {order.Id}");
        message.AppendLine($"\n🕒 Дата оформления: {order.Created:dd.MM.yyyy HH:mm}");
        message.AppendLine($"\n👤 Информация о клиенте:");
        message.AppendLine($"📞 Номер телефона: {order.PhoneNumber ?? "не указан"}");
        message.AppendLine($"🏢 Название организации: {order.OrganisationName ?? "не указано"}");
        message.AppendLine($"\n💰 Общая сумма: {order.TotalPrice} сум");
        message.AppendLine("\n📋 Товары:");

        foreach (var item in order.Items)
        {
            message.AppendLine($"🖥️ Название: {item.ProductName}");
            message.AppendLine($"💵 Цена: {item.Price} сум");
            message.AppendLine($"⏱️ Длительность показа: {item.RentTime?.RentSeconds} секунд");
            message.AppendLine($"📅 Срок аренды: {item.RentTime?.RentMonths} месяцев");
            message.AppendLine("➖➖➖➖➖➖➖➖➖");
        }

        await SendMessageAsync(message.ToString());
    }
}


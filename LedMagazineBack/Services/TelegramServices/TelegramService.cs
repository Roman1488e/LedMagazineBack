using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.TelegramServices.Abstract;

namespace LedMagazineBack.Services.TelegramServices;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class TelegramService(HttpClient httpClient, IUnitOfWork unitOfWork) : ITelegramService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private const string BotToken = "8189559523:AAGYnnD3GCUWM3a8FRtVENBAybI021dRj14";
    private const string ChatId = "527422045";

    public async Task SendMessageAsync(string message)
    {
        const string url = $"https://api.telegram.org/bot{BotToken}/sendMessage";

        var payload = new
        {
            chat_id = ChatId,
            text = message
        };

        var content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();
    }

    public async Task GenerateMessageAsync(Guid orderId)
    {
        var order = await _unitOfWork.OrderRepository.GetById(orderId);

        var message = new StringBuilder();
        message.AppendLine($"#Order {order.OrderNumber}");
        message.AppendLine($"Order Id: {order.Id}");
        message.AppendLine($"\nDate: {order.Created}");
        message.AppendLine($"\nClient info:\n-Phone Number: {order.PhoneNumber}.\n-Organisation Name: {order.OrganisationName}.");
        message.AppendLine($"\nPrice: {order.TotalPrice}");
        message.AppendLine("\nItems:");

        foreach (var item in order.Items)
        {
            message.AppendLine($"Name - {item.ProductName}");
            message.AppendLine($"Price - {item.Price}");
            message.AppendLine($"Rent for {item.RentTime.RentSeconds} seconds");
            message.AppendLine($"Rent for {item.RentTime.RentMonths} months");
            message.AppendLine("==========================");
        }

        await SendMessageAsync(message.ToString());
    }

}

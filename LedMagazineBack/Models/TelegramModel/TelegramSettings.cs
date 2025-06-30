namespace LedMagazineBack.Models.TelegramModel;

public class TelegramSettings
{
    public string BotToken { get; set; } = null!;
    public List<string> ChatIds { get; set; } = new();
}

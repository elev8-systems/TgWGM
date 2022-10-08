namespace TelegramBot.Services.Bot;

public interface ITelegramBotClientService
{
    void Run();
    void Stop();
}
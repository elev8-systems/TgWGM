using Microsoft.Extensions.Hosting;
using TelegramBot.Services.Bot;

namespace TelegramBot.HostedServices;

public class TelegramBotHostedService : IHostedService
{
    private readonly ITelegramBotClientService _telegramBotClient;

    public TelegramBotHostedService(ITelegramBotClientService telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _telegramBotClient.Run();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _telegramBotClient.Stop();
    }
}
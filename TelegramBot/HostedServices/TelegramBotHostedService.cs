using Microsoft.Extensions.Hosting;
using TelegramBot.Services.Bot;
using TelegramBot.Services.Remote;

namespace TelegramBot.HostedServices;

public class TelegramBotHostedService : IHostedService
{
    private readonly ITelegramBotClientService _telegramBotClient;
    private readonly IRemoteService _remoteService;

    public TelegramBotHostedService(ITelegramBotClientService telegramBotClient, IRemoteService remoteService)
    {
        _telegramBotClient = telegramBotClient;
        _remoteService = remoteService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _telegramBotClient.Run();
        _remoteService.Run();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _telegramBotClient.Stop();
    }
}
using Microsoft.Extensions.DependencyInjection;
using Shared.Container;
using TelegramBot.Services.Bot;
using TelegramBot.Services.Remote;

namespace TelegramBot;

internal class Startup : IContainerStartup
{
    public void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITgBot, TgBot>();
        serviceCollection.AddSingleton<IRemoteService, SshRemote>();
    }

    public void ConfigureServices(IServiceProvider serviceProvider)
    {
    }
}
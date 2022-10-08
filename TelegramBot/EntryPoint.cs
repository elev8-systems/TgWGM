using Shared.Container;
using TelegramBot.Services.Bot;
using TelegramBot.Services.Remote;

namespace TelegramBot;

internal class EntryPoint : IContainerEntryPoint
{
    private readonly ITgBot _bot;
    private readonly IRemoteService _remote;

    public EntryPoint(ITgBot bot, IRemoteService remote)
    {
        _bot = bot;
        _remote = remote;
    }
    
    public void Run()
    {
        Console.WriteLine("Hello there");
        _bot.Run();
        _remote.Init();
    }
}
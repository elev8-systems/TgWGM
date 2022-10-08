using Microsoft.Extensions.Hosting;

namespace TelegramBot;

public class HostedServiceExample : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Hello world");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Bye bye world");
    }
}
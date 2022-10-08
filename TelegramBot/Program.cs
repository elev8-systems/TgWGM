using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using TelegramBot.HostedServices;
using TelegramBot.Services.Bot;
using TelegramBot.Settings;

// Entry point for ARNetManage

// Logging

var rootLogger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

// Generic host configuration

var host = Host.CreateDefaultBuilder(args)
    .ConfigureDefaults(args)
    .ConfigureLogging(c =>
    {
        c.ClearProviders();
        c.AddSerilog(rootLogger);
    })
    .ConfigureServices((context, services) =>
    {
        // Hosted services
        services.AddHostedService<TelegramBotHostedService>();

        // Services
        services.AddSingleton<ITelegramBotClientService, TelegramBotClientService>();

        // Settings
        services.Configure<TelegramBotSettings>(context.Configuration.GetSection("Telegram"));
    })
    .Build();

await host.RunAsync();
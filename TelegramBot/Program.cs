using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using TelegramBot.Database;
using TelegramBot.HostedServices;
using TelegramBot.Services.Bot;
using TelegramBot.Services.Database;
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
        var configuration = context.Configuration;
        
        // Hosted services
        services.AddHostedService<TelegramBotHostedService>();

        // Services
        services.AddSingleton<IDatabaseMigratorService, DatabaseMigratorService>();
        services.AddSingleton<ITelegramBotClientService, TelegramBotClientService>();
        
        // Database
        services.AddDbContextFactory<MainContext>(config =>
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                config.UseSqlite(configuration.GetConnectionString("Development"));
                return;
            }

            config.UseNpgsql(configuration.GetConnectionString("Production"));
        }, ServiceLifetime.Transient);

        // Settings
        services.Configure<TelegramBotSettings>(configuration.GetSection("Telegram"));
    })
    .Build();

// Database migration
var migratorService = host.Services.GetRequiredService<IDatabaseMigratorService>();
await migratorService.Migrate(default);

// Finally starting the bot
await host.RunAsync();

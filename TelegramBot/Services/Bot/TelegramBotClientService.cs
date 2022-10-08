using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Extensions;
using TelegramBot.Settings;

namespace TelegramBot.Services.Bot;

public class TelegramBotClientService : ITelegramBotClientService
{
    private readonly ILogger<TelegramBotClientService> _logger;
    private readonly TelegramBotClient _botClient;
    private readonly CancellationTokenSource _botCancelToken;
    private readonly ManualResetEvent _botStoppedEvent = new(false);
    
    public TelegramBotClientService(ILogger<TelegramBotClientService> logger, IOptions<TelegramBotSettings> telegramOptions)
    {
        var telegramBotToken = telegramOptions.Value.BotToken;

        if (string.IsNullOrWhiteSpace(telegramBotToken))
        {
            logger.LogCritical("Telegram Bot token is not provided!");
            throw new InvalidOperationException();
        }
        
        _logger = logger;
        _botClient = new TelegramBotClient(telegramBotToken);
        _botCancelToken = new CancellationTokenSource();
    }
    
    public void Run()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        
        _botClient.StartReceivingNotifiable(
            new DefaultUpdateHandler(HandleUpdateAsync, HandlePollingErrorAsync),
            receiverOptions,
            _botCancelToken.Token,
            _botStoppedEvent
        );
    }

    public void Stop()
    {
        _botCancelToken.Cancel();
        _botStoppedEvent.WaitOne();
    }

    private async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;
        
        if (message.Text is not { } messageText)
            return;

        if(message.Chat.Type != ChatType.Private)
            return;
        
        var chatId = message.Chat.Id;
        
        _logger.LogInformation(@"Received a '{MessageText}' message in chat {ChatId}", messageText, chatId);
        
        // Echo received message text
        // var sentMessage = await _botClient.SendTextMessageAsync(
        //     chatId: chatId,
        //     text: "You said:\n" + messageText,
        //     cancellationToken: cancellationToken,
        //     replyMarkup: replyKeyboardMarkup);
    }
}
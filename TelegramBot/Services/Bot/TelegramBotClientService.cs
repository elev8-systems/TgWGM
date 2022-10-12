using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
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
        
        logger.LogInformation("Set Admin Chat ID: {AdminChatId}", telegramOptions.Value.AdminChatId);

        if (telegramOptions.Value.AdminChatId == 0)
        {
            logger.LogWarning("Admin Chat ID is set to 0. Change this if you want to control bot using chat");
        }
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

        RegisterCommands().Wait();
    }

    public void Stop()
    {
        _botCancelToken.Cancel();
        _botStoppedEvent.WaitOne();
    }

    private async Task RegisterCommands()
    {
        await _botClient.DeleteMyCommandsAsync();
        
        await _botClient.SetMyCommandsAsync(new[]
        {
            new BotCommand
            {
                Command = "menu",
                Description = "Call a menu"
            }
        }, scope: BotCommandScope.Chat(577923881));
    }

    private async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogError("An error occured:\n{ErrorMessage}", errorMessage);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        if (update.CallbackQuery != null)
        {
            _logger.LogDebug(@"Query data '{Data}', QueryId '{QueryId}', MessageId {MessageId}", 
                update.CallbackQuery.Data, update.CallbackQuery.Id, update.CallbackQuery.Message?.MessageId);
        }
        
        if (update.Message is not { } message)
            return;
        
        if (message.Text is not { } messageText)
            return;

        if(message.Chat.Type != ChatType.Private)
            return;
        
        var chatId = message.Chat.Id;
        
        _logger.LogDebug(@"Received a '{MessageText}' message in chat {ChatId}. User {User}", messageText, chatId, update.Message.From?.Id);
        
        if(messageText != "/menu")
            return;

        // var msg = await _botClient.SendTextMessageAsync(
        //     chatId, "Menu", ParseMode.MarkdownV2,
        //     disableNotification: true,
        //     replyToMessageId: update.Message.MessageId,
        //     replyMarkup: new InlineKeyboardMarkup(new []
        //     {
        //         InlineKeyboardButton.WithCallbackData("1w", "SHIET"), InlineKeyboardButton.WithCallbackData("2", "SHIET")
        //     }),
        //     cancellationToken: cancellationToken);
        
    }
}
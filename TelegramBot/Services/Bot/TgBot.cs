using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services.Bot;

public class TgBot : ITgBot
{
    private readonly TelegramBotClient _botClient;
    private readonly CancellationTokenSource _botCancelToken;
    
    // TODO Add dependencies
    public TgBot()
    {
        // TODO Get token from env
        _botClient = new TelegramBotClient("5618597824:AAHXbCtNZTMyQQgT3sSA40KBmz0IlmQ25SE");
        _botCancelToken = new CancellationTokenSource();
    }
    
    public void Run()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        
        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: _botCancelToken.Token
        );
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
        
        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        
        // Echo received message text
        // var sentMessage = await _botClient.SendTextMessageAsync(
        //     chatId: chatId,
        //     text: "You said:\n" + messageText,
        //     cancellationToken: cancellationToken,
        //     replyMarkup: replyKeyboardMarkup);
    }
}
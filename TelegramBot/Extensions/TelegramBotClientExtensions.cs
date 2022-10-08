using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TelegramBot.Extensions;

public static class TelegramBotClientExtensions
{
    /// <summary>Starts receiving <see cref="T:Telegram.Bot.Types.Update" />s on the ThreadPool, invoking <see cref="M:Telegram.Bot.Polling.IUpdateHandler.HandleUpdateAsync(Telegram.Bot.ITelegramBotClient,Telegram.Bot.Types.Update,System.Threading.CancellationToken)" /> for each. <para>This method does not block. GetUpdates will be called AFTER the <see cref="M:Telegram.Bot.Polling.IUpdateHandler.HandleUpdateAsync(Telegram.Bot.ITelegramBotClient,Telegram.Bot.Types.Update,System.Threading.CancellationToken)" /> returns</para> This method will notify when all updates will be processed.</summary>
    /// <param name="botClient">The <see cref="T:Telegram.Bot.ITelegramBotClient" /> used for making GetUpdates calls</param>
    /// <param name="updateHandler">The <see cref="T:Telegram.Bot.Polling.IUpdateHandler" /> used for processing <see cref="T:Telegram.Bot.Types.Update" />s</param>
    /// <param name="receiverOptions">Options used to configure getUpdates request</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> with which you can stop receiving</param>
    /// <param name="receivingStopped">The <see cref="ManualResetEvent"/> which will be set when bot finishes processing incoming updates.</param>
    public static void StartReceivingNotifiable(
        this ITelegramBotClient botClient,
        IUpdateHandler updateHandler,
        ReceiverOptions? receiverOptions = null,
        CancellationToken cancellationToken = default,
        ManualResetEvent? receivingStopped = null)
    {
        if (botClient == null)
        {
            throw new ArgumentNullException(nameof(botClient));
        }

        if (updateHandler == null)
        {
            throw new ArgumentNullException(nameof(updateHandler));
        }

        Task.Run((Func<Task>)(async () =>
        {
            try
            {
                await botClient.ReceiveAsync(updateHandler, receiverOptions, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex1)
            {
                try
                {
                    await updateHandler.HandlePollingErrorAsync(botClient, ex1, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                }
            }

            receivingStopped?.Set();
        }));
    }
}
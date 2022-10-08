using System.Diagnostics.CodeAnalysis;

namespace TelegramBot.Settings;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instanciated by reflection")]
public class TelegramBotSettings
{
    public string? BotToken { get; set; }
}
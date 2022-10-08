namespace TelegramBot.Services.Database;

public interface IDatabaseMigratorService
{
    Task Migrate(CancellationToken cancellationToken);
}
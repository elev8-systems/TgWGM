using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TelegramBot.Database;

namespace TelegramBot.Services.Database;

public class DatabaseMigratorService : IDatabaseMigratorService
{
    private readonly IDbContextFactory<MainContext> _contextFactory;
    private readonly ILogger<DatabaseMigratorService> _logger;

    public DatabaseMigratorService(IDbContextFactory<MainContext> contextFactory, ILogger<DatabaseMigratorService> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }
    
    public async Task Migrate(CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var pendingMigrations = (await context.Database.GetPendingMigrationsAsync(cancellationToken)).ToArray();

        if (!pendingMigrations.Any())
        {
            _logger.LogInformation("Migration is not needed");
            return;
        }
        
        _logger.LogInformation("Pending migrations ({MigrationCount}): {MigrationList}", pendingMigrations.Length, string.Join(", ", pendingMigrations));
        await context.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Migration complete");
    }
}
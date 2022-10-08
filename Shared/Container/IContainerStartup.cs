using Microsoft.Extensions.DependencyInjection;

namespace Shared.Container;

public interface IContainerStartup
{
    void RegisterServices(IServiceCollection serviceCollection);
    void ConfigureServices(IServiceProvider serviceProvider);
}
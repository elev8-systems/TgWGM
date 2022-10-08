using Shared.Container;
using TelegramBot;

var container = new AppContainer<Startup, EntryPoint>();

container.Initialize();
container.Run();

Console.ReadLine();

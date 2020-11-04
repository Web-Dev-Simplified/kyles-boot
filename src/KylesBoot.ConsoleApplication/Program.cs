using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using KylesBoot.Logic;

namespace KylesBoot.ConsoleApplication
{
    public class Program
    {
        private static async Task Main()
        {
            var serviceProvider = GetServiceProvider();
            var bot = serviceProvider.GetService<IKylesBootDiscordBot>();

            await bot.StartAsync();

            // Wait indefinitely for now
            await Task.Delay(-1);
        }

        private static IServiceProvider GetServiceProvider()
        {
            // Add new types here
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IKylesBootDiscordBot, KylesBootDiscordBot>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}

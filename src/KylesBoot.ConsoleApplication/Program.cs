using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using KylesBoot.Logic;
using KylesBoot.Logic.Configuration;
using Microsoft.Extensions.Configuration;

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


            // Set up configuration
            var configurationPath = GetConfigurationPath();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configurationPath)
                .Build();

            var discordConfiguration = new DiscordConfiguration();
            configuration.Bind("DiscordConfiguration", discordConfiguration);
            serviceCollection.AddSingleton<IDiscordConfiguration>(discordConfiguration);

            return serviceCollection.BuildServiceProvider();
        }

        private static string GetConfigurationPath()
        {
#if DEBUG
            return @"appsettings.Development.json";
#else
            return @"appsettings.json";
#endif
        }
    }
}

using System;
using System.Threading.Tasks;
using KylesBoot.Logic.Configuration;

namespace KylesBoot.Logic
{
    public interface IKylesBootDiscordBot
    {
        Task StartAsync();
        Task StopAsync();
    }

    public class KylesBootDiscordBot : IKylesBootDiscordBot
    {
        private IDiscordConfiguration _discordConfig;

        public KylesBootDiscordBot(IDiscordConfiguration discordConfig)
        {
            _discordConfig = discordConfig;
        }

        public Task StartAsync()
        {
            Console.WriteLine("Started!");
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Threading.Tasks;

namespace KylesBoot.Logic
{
    public interface IKylesBootDiscordBot
    {
        Task StartAsync();
        Task StopAsync();
    }

    public class KylesBootDiscordBot : IKylesBootDiscordBot
    {
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

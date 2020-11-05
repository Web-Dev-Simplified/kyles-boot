using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using KylesBoot.Logic.Configuration;

namespace KylesBoot.Logic
{
    public interface IKylesBootDiscordBot
    {
        Task StartAsync(IServiceProvider? serviceProvider);
        Task StopAsync();
    }

    public class KylesBootDiscordBot : IKylesBootDiscordBot
    {
        private readonly IDiscordConfiguration _discordConfig;
        private IServiceProvider? _serviceProvider;

        private readonly DiscordSocketClient _client;
        private readonly CommandHandler _commandHandler;

        public KylesBootDiscordBot(IDiscordConfiguration discordConfig)
        {
            _discordConfig = discordConfig;

            _client = new DiscordSocketClient();
            _commandHandler = new CommandHandler(_client, new CommandService(), discordConfig);
        }

        public async Task StartAsync(IServiceProvider? serviceProvider = null)
        {
            _serviceProvider = serviceProvider;
            _client.Ready += SetupAsync;
            _client.Log += LogToConsole;

            await _client.LoginAsync(TokenType.Bot, _discordConfig.BotToken);
            await _client.StartAsync();
        }

        private async Task SetupAsync()
        {
            await _commandHandler.RegisterCommands(_serviceProvider);
            await _client.SetGameAsync("for scam bots 👀", type: ActivityType.Watching);
        }

        private Task LogToConsole(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return _client.LogoutAsync();
        }
    }
}

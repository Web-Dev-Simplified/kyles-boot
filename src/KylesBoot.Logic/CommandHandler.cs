using Discord.Commands;
using Discord.WebSocket;
using KylesBoot.Logic.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace KylesBoot.Logic
{
    public interface ICommandHandler
    {
        Task RegisterCommands(IServiceProvider serviceProvider);
    }

    public class CommandHandler : ICommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly IDiscordConfiguration _discordConfiguration;

        private IServiceProvider? _serviceProvider;

        public CommandHandler(DiscordSocketClient client, CommandService commandService, IDiscordConfiguration discordConfiguration)
        {
            _client = client;
            _commandService = commandService;
            _discordConfiguration = discordConfiguration;
        }

        public async Task RegisterCommands(IServiceProvider? serviceProvider)
        {
            _client.MessageReceived += HandleCommandAsync;

            _serviceProvider = serviceProvider;
            var moduleInfo = await _commandService.AddModulesAsync(Assembly.GetExecutingAssembly(), serviceProvider);
        }

        private async Task HandleCommandAsync(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message)) { return; }
            if (message.Author.IsBot) { return; }

            var argPos = 0;
            if (!message.HasStringPrefix(_discordConfiguration.CommandPrefix, ref argPos) &&
                !message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            { 
                return; 
            }

            var context = new SocketCommandContext(_client, message);

            var result = await _commandService.ExecuteAsync(context, argPos, _serviceProvider);

            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}

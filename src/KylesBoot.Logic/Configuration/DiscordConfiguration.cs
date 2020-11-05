namespace KylesBoot.Logic.Configuration
{
    public interface IDiscordConfiguration
    {
        string BotToken { get; }
        string CommandPrefix { get; }
        ulong NewMembersChannelId { get; }
    }

    public class DiscordConfiguration : IDiscordConfiguration
    {
        public string BotToken { get; set; } = "";
        public string CommandPrefix { get; set; } = "";
        public ulong NewMembersChannelId { get; set; }
    }
}

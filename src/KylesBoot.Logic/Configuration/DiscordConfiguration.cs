namespace KylesBoot.Logic.Configuration
{
    public interface IDiscordConfiguration
    {
        string BotToken { get; }
    }

    public class DiscordConfiguration : IDiscordConfiguration
    {
        public string BotToken { get; set; } = "";
    }
}

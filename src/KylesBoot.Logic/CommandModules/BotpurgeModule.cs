using Discord;
using Discord.Commands;
using KylesBoot.Logic.Configuration;
using System;
using System.Threading.Tasks;

namespace KylesBoot.Logic.CommandModules
{
    public class BotpurgeModule : ModuleBase<SocketCommandContext>
    {
        private readonly IDiscordConfiguration _discordConfiguration;

        public BotpurgeModule(IDiscordConfiguration discordConfiguration)
        {
            _discordConfiguration = discordConfiguration;
        }

        [Command("botpurge between")]
        [Alias("botpurge", "botpurgebetween")]
        public async Task BotPurgeBetween(ulong message1Id, ulong message2Id, [Remainder] string args = "")
        {
            var newMembersChannel = Context.Guild.GetTextChannel(_discordConfiguration.NewMembersChannelId);

            if (newMembersChannel == null)
            {
                throw new NullReferenceException("Invalid channel ID for #new-members text channel.");
            }

            // Todo: notify the users if the message IDs are the same

            var message1 = await newMembersChannel.GetMessageAsync(message1Id);
            var message2 = await newMembersChannel.GetMessageAsync(message2Id);

            if (message1 == null || message2 == null)
            { 
                throw new ArgumentException("At least one of your messages are not in the #new-members channel.");
            }

            Direction direction = message1.CreatedAt > message2.CreatedAt
                ? Direction.Before
                : Direction.After;

            var messages = await newMembersChannel.GetMessagesAsync(message1, direction).FlattenAsync(); // Default 100 messages
            // Does not include message1
            // Order is reversed so the [0]th message is the one furthest away from message1 I think?
            // Might want to order these messages on timestamp just to make sure with LINQ

            // The messages will have to be iterated until message2 is reached or the end of the list
            // If it reaches the end of the list we need to notify the user of this and say between which
            // two messages the bots are going to be kicked

            // Maybe there should always be a notification between what messages the bots will be kicked
            // Ultimately it would also have a confirm/cancel where at least 2 moderators, or kyle, or me have to confirm.
            // It would then add a reaction with X and checkmark and keep track somewhere that this has happened

            // Then catch this when OnReactionAdded
            
            foreach(var message in messages)
            {
                Console.WriteLine(message.Timestamp + " " + message.Content);
            }
        }
    }
}

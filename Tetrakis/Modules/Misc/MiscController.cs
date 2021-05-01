using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Tetrakis.Modules.Misc
{
    public static class MiscController
    {
        public static ulong LazuriteGuild = 719662192601071747;
        public static ulong LazuriteNewsChannel = 719669273621954641;
        
        public static void Register(DiscordClient discord)
        {
            // Register misc commands
            discord.GetCommandsNext().RegisterCommands<MiscCommands>();
            
            // Register the member join listener
            discord.GuildMemberAdded += OnMemberJoin;
            
            // Food pics :yum:
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Channel.Name.Equals("food-pics") &&
                    e.Message.Attachments.Count > 0 &&
                    (e.Message.Attachments[0].FileName.Contains(".png") || e.Message.Attachments[0].FileName.Contains(".jpg")))
                {
                    await e.Message.CreateReactionAsync(DiscordEmoji.FromName(discord, ":camera_with_flash:"));
                }
            };
        }

        public static async Task OnMemberJoin(DiscordClient client, GuildMemberAddEventArgs args)
        {
            if (args.Guild.Id == LazuriteGuild)
            {
                await args.Guild.Channels[LazuriteNewsChannel].SendMessageAsync($"New member: {args.Member.DisplayName}");
            }
        }
    }
}
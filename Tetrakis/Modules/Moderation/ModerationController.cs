using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Tetrakis.Constants;

namespace Tetrakis.Modules.Moderation
{
    public static class ModerationController
    {
        public static void Register(DiscordClient discord)
        {
            // Register moderation commands
            discord.GetCommandsNext().RegisterCommands<ModerationCommands>();
            
            // Register the member join listener
            // discord.GuildMemberAdded += OnMemberJoin;
        }

        // private static async Task OnMemberJoin(DiscordClient client, GuildMemberAddEventArgs args)
        // {
            // if (LazuriteGuild.Equals(args.Guild))
            // {
                // await LazuriteGuild.GetChannel(args.Guild, "news").SendMessageAsync($"New member: {args.Member.DisplayName}");
            // }
        // }
    }
}
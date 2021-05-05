using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Tetrakis.Modules.Moderation
{
    public class ModerationCommands : BaseCommandModule
    {
        [Command("members")]
        public async Task Members(CommandContext ctx)
        {
            if (ctx.Guild != null)
            {
                await ctx.RespondAsync(new DiscordMessageBuilder()
                {
                    Content = $"There are {ctx.Guild.MemberCount} members in {ctx.Guild.Name}"
                });
            }
        }
    }
}
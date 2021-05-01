﻿using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Tetrakis.Modules.Misc
{
    public class MiscCommands : BaseCommandModule
    {
        [Command("members")]
        public async Task Members(CommandContext ctx)
        {
            if (ctx.Guild == null)
            {
                await ctx.RespondAsync(new DiscordMessageBuilder()
                {
                    Content =  "This command is only available within discord servers."
                });
            }
            else
            {
                await ctx.RespondAsync(new DiscordMessageBuilder()
                {
                    Content = $"There are {ctx.Guild.MemberCount} members in {ctx.Guild.Name}"
                });
            }
        }
    }
}
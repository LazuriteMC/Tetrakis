using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Tetrakis.Modules.Highlight
{
    public class HighlightCommands : BaseCommandModule
    {
        [Command("hl")]
        public async Task Hl(CommandContext ctx, string word)
        {
            if (ctx.Guild != null)
            {
                await HighlightData.NewHighlight(ctx.Member.Id, ctx.Guild.Id, word);
            }
        }
    }
}
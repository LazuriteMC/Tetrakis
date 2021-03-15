using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace LazuriteBot
{
    public class LibHelpCommands : BaseCommandModule
    {
        [Command("rayonhelp")]
        public async Task RayonHelpCommand(CommandContext context)
        {
            await context.RespondAsync("https://docs.lazurite.dev/rayon/getting-started");
        }
    }
}
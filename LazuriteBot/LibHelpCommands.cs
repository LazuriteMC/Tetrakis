using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace LazuriteBot
{
    public class LibHelpCommands : BaseCommandModule
    {
        // <summary>
        // A command which provides information about using Rayon.
        // </summary>
        [Command("rayon")]
        public async Task RayonCommand(CommandContext context)
        {
            await context.RespondAsync("https://docs.lazurite.dev/rayon/getting-started");
        }
        
        // <summary>
        // A command which provides information about using Transporter.
        // </summary>
        [Command("transporter")]
        public async Task TransporterCommand(CommandContext context)
        {
            await context.RespondAsync("https://docs.lazurite.dev/transporter/getting-started");
        }
    }
}
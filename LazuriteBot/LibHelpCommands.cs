using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

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
            await context.RespondAsync("Rayon is a rigid body simulation library designed to work with the Fabric API.\n\nhttps://docs.lazurite.dev/rayon/getting-started");
        }
        
        // <summary>
        // A command which provides information about using Transporter.
        // </summary>
        [Command("transporter")]
        public async Task TransporterCommand(CommandContext context)
        {
            await context.RespondAsync("Transporter is a small library for Fabric which allows you to send block, entity, and item renderer information to the server from the client.\n\nhttps://docs.lazurite.dev/transporter/getting-started");
        }
    }
}
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace LazuriteBot
{
    public class LibHelpCommands : BaseCommandModule
    {
        // <summary>
        // A command which provides information about Lazurite.
        // </summary>
        [Command("lazurite")]
        public async Task LazuriteCommand(CommandContext context)
        {
            await context.RespondAsync("Lazurite is a team of two developers who originally worked together on Quadz (formerly FPV Racing).\n\nhttps://lazurite.dev/");
        }
        
        // <summary>
        // A command which provides a link to the mixin docs
        // </summary>
        [Command("mixindocs")]
        public async Task MixinDocsCommand(CommandContext context)
        {
            await context.RespondAsync("https://jenkins.liteloader.com/view/Other/job/Mixin/javadoc/index.html");
        }
        
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
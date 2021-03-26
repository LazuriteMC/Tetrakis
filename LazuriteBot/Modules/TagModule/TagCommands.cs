using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace LazuriteBot.Modules.TagModule
{
    public class TagCommands : BaseCommandModule
    {
        [Command("tags")]
        public async Task Tags(CommandContext ctx)
        {
            string tags = "";
            
            foreach (var key in TagController.Tags.Keys)
            {
                tags += key + '\n';
            }
            
            await ctx.RespondAsync(new DiscordEmbedBuilder()
            {
                Title = "Available Tags",
                Description = $"```{tags}```"
            });
        }
    }
}
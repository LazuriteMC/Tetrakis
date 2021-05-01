using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Tetrakis.Modules.Tag
{
    public class TagCommands : BaseCommandModule
    {
        [Command("tags")]
        public async Task Tags(CommandContext ctx)
        {
            string tags = "";
            
            foreach (var key in TagController.Tags.Keys)
            {
                tags += '\n' + key;
            }
            
            await ctx.RespondAsync(new DiscordEmbedBuilder()
            {
                Title = "Available Tags",
                Description = $"```{tags}```"
            });
        }

        [Command("reload")]
        public async Task Reload(CommandContext ctx)
        {
            TagController.Tags = TagController.Read();
            
            await ctx.RespondAsync(new DiscordMessageBuilder()
            {
                Content = "Tags reloaded successfully."
            });
        }
    }
}
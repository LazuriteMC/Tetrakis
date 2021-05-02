using DSharpPlus;
using DSharpPlus.Entities;

namespace Tetrakis.Modules.Misc
{
    public static class MiscController
    {
        public static void Register(DiscordClient discord)
        {
            // Food pics :yum:
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Channel.Name.Equals("food-pics") &&
                    e.Message.Attachments.Count > 0 &&
                    (e.Message.Attachments[0].FileName.Contains(".png") ||
                     e.Message.Attachments[0].FileName.Contains(".jpg")))
                {
                    await e.Message.CreateReactionAsync(DiscordEmoji.FromName(discord, ":camera_with_flash:"));
                }
            };
        }
    }
}
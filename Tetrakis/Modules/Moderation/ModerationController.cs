using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace Tetrakis.Modules.Moderation
{
    public static class ModerationController
    {
        public static void Register(DiscordClient discord)
        {
            // Register moderation commands
            discord.GetCommandsNext().RegisterCommands<ModerationCommands>();
        }
    }
}
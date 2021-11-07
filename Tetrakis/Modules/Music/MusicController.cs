using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using YoutubeExplode.Videos;

namespace Tetrakis.Modules.Music
{
    public static class MusicController
    {
        public static Dictionary<DiscordGuild, Queue<Video>> Queues { get; private set; }
        public static Dictionary<DiscordGuild, NowPlaying> NowPlaying { get; private set; }

        public static void Register(DiscordClient discord)
        {
            Queues = new Dictionary<DiscordGuild, Queue<Video>>();
            NowPlaying = new Dictionary<DiscordGuild, NowPlaying>();
            
            // Enable VoiceNext
            discord.UseVoiceNext();
            
            // Register moderation commands
            discord.GetCommandsNext().RegisterCommands<MusicCommands>();
        }
    }
}
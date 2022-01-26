using System;
using System.Collections.Generic;
using System.Linq;
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
            
            // Disconnect from VC if nobody is there anymore.
            discord.VoiceStateUpdated += async (s, e) =>
            {
                var connection = s.GetVoiceNext().GetConnection(e.Guild);
                if (connection != null && connection.TargetChannel == e.Channel && !e.Channel.Users.Any())
                {
                    NowPlaying[e.Guild].Cancel();
                    s.GetVoiceNext().GetConnection(e.Guild).Disconnect();
                }
            };
        }
    }
}
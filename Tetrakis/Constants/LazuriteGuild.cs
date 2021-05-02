using System.Collections.Generic;
using DSharpPlus.Entities;

namespace Tetrakis.Constants
{
    public static class LazuriteGuild
    {
        public const ulong Id = 719662192601071747;

        public static readonly Dictionary<string, ulong> Channels = new()
        {
            { "news", 719669273621954641 }
        };

        public static bool Equals(DiscordGuild guild) => guild.Id == Id;

        public static DiscordChannel GetChannel(DiscordGuild guild, string channelName) => guild.Channels[Channels[channelName]];
    }
}
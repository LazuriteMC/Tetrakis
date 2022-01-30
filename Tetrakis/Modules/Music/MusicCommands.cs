using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace Tetrakis.Modules.Music
{
    public class MusicCommands : BaseCommandModule
    {
        [Command("play")]
        public async Task Play(CommandContext ctx, [RemainingText] string arg)
        {
            var voiceNext = ctx.Client.GetVoiceNext();
            var voiceNextConnection = voiceNext.GetConnection(ctx.Guild);
            var channel = ctx.Member?.VoiceState?.Channel;
            
            if (!MusicController.Queues.ContainsKey(ctx.Guild))
            {
                MusicController.Queues.Add(ctx.Guild, new Queue<Video>());
            }

            if (channel == null)
            {
                throw new InvalidOperationException("You need to be in a voice channel.");
            }

            var queue = MusicController.Queues[ctx.Guild];
            var youtube = new YoutubeClient();
            
            if (!(Uri.TryCreate(arg, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
            {
                await foreach (var result in youtube.Search.GetVideosAsync(arg))
                {
                    arg = result.Url;
                    break;
                }
            }
            
            var video = await youtube.Videos.GetAsync(arg);
            
            voiceNextConnection ??= await voiceNext.ConnectAsync(channel);
            queue.Enqueue(video);

            if (!MusicController.NowPlaying.ContainsKey(ctx.Guild))
            {
                MusicController.NowPlaying.Add(ctx.Guild, new NowPlaying(voiceNextConnection, ctx.Guild));
                await ctx.RespondAsync("Now playing **" + video.Title + "**");
                await MusicController.NowPlaying[ctx.Guild].Play();
            }
            else
            {
                await ctx.RespondAsync("Queued **" + video.Title + "**");
            }
        }
        
        [Command("clear")]
        public async Task Clear(CommandContext ctx)
        {
            var voiceNext = ctx.Client.GetVoiceNext();
            var voiceNextConnection = voiceNext.GetConnection(ctx.Guild);
            
            if (voiceNextConnection == null)
            {
                throw new InvalidOperationException("Not connected to any channel.");
            }

            MusicController.Queues[ctx.Guild]?.Clear();
            await ctx.RespondAsync("Queue cleared.");
        }

        [Command("queue")]
        public async Task Queue(CommandContext ctx)
        {
            var voiceNext = ctx.Client.GetVoiceNext();
            var voiceNextConnection = voiceNext.GetConnection(ctx.Guild);
            var queue = MusicController.Queues[ctx.Guild];
            
            if (voiceNextConnection == null)
            {
                throw new InvalidOperationException("Not connected to any channel.");
            }

            var description = queue.Aggregate("", (current, video) => 
                current + (video.Duration + " - " + video.Title + "\n"));

            await ctx.RespondAsync(new DiscordEmbedBuilder
            {
                Title = "Queue (" + queue.Count + ")",
                Description = description
            });
        }
        
        [Command("skip")]
        public async Task Skip(CommandContext ctx)
        {
            var voiceNext = ctx.Client.GetVoiceNext();
            var voiceNextConnection = voiceNext.GetConnection(ctx.Guild);
            var queue = MusicController.Queues?[ctx.Guild];

            if (voiceNextConnection == null)
            {
                throw new InvalidOperationException("Not connected to any channel.");
            }

            MusicController.NowPlaying?[ctx.Guild]?.Cancel();
            MusicController.NowPlaying?.Remove(ctx.Guild);
            MusicController.NowPlaying?.Add(ctx.Guild, new NowPlaying(voiceNextConnection, ctx.Guild));

            if (queue?.Count > 0)
            {
                MusicController.NowPlaying?[ctx.Guild].Play();
            }
            else
            {
                MusicController.NowPlaying?.Remove(ctx.Guild);
                voiceNextConnection.Disconnect();
                voiceNextConnection.Dispose();
            }
        }

        [Command("stop")]
        public async Task Stop(CommandContext ctx)
        {
            var voiceNext = ctx.Client.GetVoiceNext();
            var voiceNextConnection = voiceNext.GetConnection(ctx.Guild);

            if (voiceNextConnection == null)
            {
                throw new InvalidOperationException("Not connected to any channel.");
            }

            MusicController.NowPlaying?.Remove(ctx.Guild);
            voiceNextConnection.Disconnect();
            voiceNextConnection.Dispose();
        }

        [Command("stfu")]
        public async Task Stfu(CommandContext ctx)
        {
            var voiceNext = ctx.Client.GetVoiceNext();
            var voiceNextConnection = voiceNext.GetConnection(ctx.Guild);
            
            if (voiceNextConnection == null)
            {
                throw new InvalidOperationException("Not connected to any channel.");
            }

            MusicController.NowPlaying?.Remove(ctx.Guild);
            voiceNextConnection.Disconnect();
            voiceNextConnection.Dispose();
        }
    }
}
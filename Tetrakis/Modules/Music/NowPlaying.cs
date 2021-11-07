using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Tetrakis.Modules.Music
{
    public class NowPlaying
    {
        private readonly CancellationTokenSource _cancellationSource;
        private readonly VoiceNextConnection _connection;
        private readonly DiscordGuild _guild;

        public NowPlaying(VoiceNextConnection connection, DiscordGuild guild)
        {
            _cancellationSource = new();
            _connection = connection;
            _guild = guild;
        }

        public void Cancel()
        {
            _cancellationSource.Cancel();
        }

        public async Task Play()
        {
            var youtube = new YoutubeClient();
            var queue = MusicController.Queues[_guild];

            while (!_cancellationSource.IsCancellationRequested && queue.Count > 0)
            {
                // Get video stream data
                var video = queue.Dequeue();
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);

                // Convert!
                var psi = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $@"-i pipe:.mp3 -ac 2 -f s16le -ar 48000 pipe:1 -loglevel panic",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false
                };

                var ffmpeg = Process.Start(psi);
                var ffin = ffmpeg.StandardInput.BaseStream;
                var ffout = ffmpeg.StandardOutput.BaseStream;

                // sketchy af
                stream.CopyToAsync(ffin, _cancellationSource.Token);

                // Play!!!!
                var transmit = _connection.GetTransmitSink();
                await ffout.CopyToAsync(transmit, cancellationToken: _cancellationSource.Token);
            }

        }
    }
}
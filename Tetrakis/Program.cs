using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;
using Tetrakis.Modules.Misc;
using Tetrakis.Modules.Moderation;
using Tetrakis.Modules.Music;
using Tetrakis.Modules.Tag;

namespace Tetrakis
{
    internal static class Program
    {
        public static string TagPath { private set; get; }

        private static void Main(string[] args)
        {
	        if (args.Length < 2)
	        {
	            Console.WriteLine("Please enter the token and tag path");
		        Environment.Exit(-1);
	        }

	        TagPath = args[1];
            MainAsync(args[0]).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Handles the main asynchronous execution and handling of command events.
        /// </summary>
        private static async Task MainAsync(string token)
        {
           // Set up client
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });
            
            // Set up command prefix
            discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });
            
            // Register module controllers
            TagController.Register(discord);
            ModerationController.Register(discord);
            MusicController.Register(discord);
            MiscController.Register(discord);

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}

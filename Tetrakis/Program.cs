using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;
using Tetrakis.Modules.Misc;
using Tetrakis.Modules.Moderation;
using Tetrakis.Modules.Tag;
using Tetrakis.Modules.Highlight;

namespace Tetrakis
{
    internal static class Program
    {
        public static string TagPath { private set; get; }
        public static string DBPath { private set; get; }

        private static void Main(string[] args)
        {
	        if (args.Length < 3)
	        {
	            Console.WriteLine("Please enter the token, tag path, and db path.");
		        Environment.Exit(-1);
	        }

	        TagPath = args[1];
            DBPath = args[2];
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
            HighlightController.Register(discord);
            ModerationController.Register(discord);
            MiscController.Register(discord);

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}

using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using LazuriteBot.Modules.ReactionModule;
using LazuriteBot.Modules.TagModule;
using Microsoft.Extensions.Logging;

namespace LazuriteBot
{
    class Program
    {
	    public static string TagPath { set; get; }

        static void Main(string[] args)
        {
	        if (args.Length < 2)
	        {
	            Console.WriteLine("Please enter the token and the tag path.");
		        Environment.Exit(-1);
	        }

	        TagPath = args[1];
            MainAsync(args[0]).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Handles the main asynchronous execution and handling of command events.
        /// </summary>
        static async Task MainAsync(string token)
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });
            
            TagController.Register(discord);
            ReactionController.Register(discord);
            
            await discord.ConnectAsync(new DiscordActivity("Minecraft", ActivityType.Playing));
            await Task.Delay(-1);
        }
    }
}

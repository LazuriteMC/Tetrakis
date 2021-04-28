using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using LazuriteBot.Modules.ReactionModule;
using LazuriteBot.Modules.TagModule;
using LazuriteBot.Modules.WikiModule;
using Microsoft.Extensions.Logging;

namespace LazuriteBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Handles the main asynchronous execution and handling of command events.
        /// </summary>
        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = File.ReadAllText("Resources/token.txt"),
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });
            
            TagController.Register(discord);
            WikiController.Register(discord);
            ReactionController.Register(discord);
            
            await discord.ConnectAsync(new DiscordActivity("Minecraft", ActivityType.Playing));
            await Task.Delay(-1);
        }
    }
}
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;

namespace LazuriteBot
{
    class Program
    {
        private static string token;
        
        static void Main(string[] args)
        {
            token = File.ReadAllText("token.txt");
            MainAsync().GetAwaiter().GetResult();
        }

        // <summary>
        // Handles the main asynchronous execution and handling of command events.
        // </summary>
        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });
            
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            { 
                StringPrefixes = new[] { "??" }
            });
            
            commands.RegisterCommands<LibHelpCommands>();
            
            // Food pics :yum:
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Channel.Name.Equals("food-pics") &&
                    e.Message.Attachments.Count > 0 &&
                    (e.Message.Attachments[0].FileName.Contains(".png") || e.Message.Attachments[0].FileName.Contains(".jpg")))
                {
                    await e.Message.CreateReactionAsync(DiscordEmoji.FromName(discord, ":camera_with_flash:"));
                }
            };
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
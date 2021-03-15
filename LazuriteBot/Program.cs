using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
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
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using DSharpPlus;
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

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });
            
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                }
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
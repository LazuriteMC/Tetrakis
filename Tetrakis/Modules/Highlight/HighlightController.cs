using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Tetrakis.Modules.Highlight
{
    public static class HighlightController
    {
        public static void Register(DiscordClient discord)
        {
            discord.GetCommandsNext().RegisterCommands<HighlightCommands>();
            discord.GuildMemberRemoved += async (sender, args) => await HighlightData.RemoveUser(args.Member.Id);
            discord.MessageCreated += OnMessage;
        }
        
        private static async Task OnMessage(DiscordClient client, MessageCreateEventArgs args)
        {
            if (args.Guild != null && !args.Message.Content.StartsWith("!"))
            {
                foreach (var member in args.Guild.Members.Values)
                {
                    var words = await HighlightData.GetWords(member.Id, args.Guild.Id);

                    foreach (var word in words)
                    {
                        if (args.Message.Content.Contains(word))
                        {
                            await member.SendMessageAsync(new DiscordMessageBuilder()
                            {
                                Content = $"`{word}` was mentioned in {args.Guild.Name}\n" + args.Message.JumpLink
                            });
                        }
                    }
                }
            }
        }
    }
}
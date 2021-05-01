using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace Tetrakis.Modules.Tag
{
    public static class TagController
    {
        public static Dictionary<string, Tetrakis.Modules.Tag.Model.Tag> Tags { get; set; }
        
        public static void Register(DiscordClient discord)
        {
            // Read in all tags
            Tags = Read();
            
            // Register tag commands
            discord.GetCommandsNext().RegisterCommands<TagCommands>();
            
            // Register tag message handler
            discord.MessageCreated += OnMessage;
        }

        private static async Task OnMessage(DiscordClient client, MessageCreateEventArgs messageEvent)
        {
            if (messageEvent.Message.Content.StartsWith("??"))
            {
                var commands = messageEvent.Message.Content[2..].Split(' ');
                var startingTag = Tags[commands[0]];

                if (startingTag != null)
                {
                    var endingTag = commands.Length > 1 ? startingTag.GetChild(commands[^1]) : startingTag;

                    if (endingTag != null)
                    {
                        await messageEvent.Message.RespondAsync($"{endingTag.Content}\n<{endingTag.Url}>");
                    }
                }
            }
        }
                
        public static Dictionary<string, Tetrakis.Modules.Tag.Model.Tag> Read()
        {
            Dictionary<string, Tetrakis.Modules.Tag.Model.Tag> output = new Dictionary<string, Tetrakis.Modules.Tag.Model.Tag>();
            string[] fileNames = Directory.GetFiles(Program.TagPath);

            foreach (var fileName in fileNames)
            {
                JsonConvert.DeserializeAnonymousType(File.ReadAllText(fileName), new {});
                output.Add(
                    new FileInfo(fileName).Name.Replace(".json", ""),
                    JsonConvert.DeserializeObject<Tetrakis.Modules.Tag.Model.Tag>(File.ReadAllText(fileName)));
            }

            return output;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using LazuriteBot.Modules.TagModule;
using Newtonsoft.Json;

namespace LazuriteBot.Modules.Tag
{
    public class TagController
    {
        public static Dictionary<string, Model.Tag> Tags { get; set; }
        
        public static void Register(DiscordClient discord)
        {
            Tags = Read();

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "??" }
            });
            
            commands.RegisterCommands<TagCommands>();
            
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.StartsWith("??"))
                {
                    var commands = e.Message.Content[2..].Split(' ');
                    var startingTag = Tags[commands[0]];
                    
                    if (startingTag != null)
                    {
                        var endingTag = commands.Length > 1 ? startingTag.GetChild(commands[^1]) : startingTag;
                        
                        if (endingTag != null)
                        {
                            await e.Message.RespondAsync($"{endingTag.Content}\n<{endingTag.Url}>");
                        }
                    }
                }
            };
        }
                
        public static Dictionary<string, Model.Tag> Read()
        {
            Dictionary<string, Model.Tag> output = new Dictionary<string, Model.Tag>();
            string[] fileNames = Directory.GetFiles(Program.TagPath);

            foreach (var fileName in fileNames)
            {
                JsonConvert.DeserializeAnonymousType(File.ReadAllText(fileName), new {});
                output.Add(
                    new FileInfo(fileName).Name.Replace(".json", ""),
                    JsonConvert.DeserializeObject<Model.Tag>(File.ReadAllText(fileName)));
            }

            return output;
        }
    }
}
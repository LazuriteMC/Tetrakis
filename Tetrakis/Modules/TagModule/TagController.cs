using System.Collections.Generic;
using System.IO;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;

namespace LazuriteBot.Modules.TagModule
{
    public class TagController
    {
        public static Dictionary<string, Tag> Tags { get; set; }
        
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
                    var name = e.Message.Content.Substring(2);

                    if (Tags.ContainsKey(name))
                    {
                        var tag = Tags[e.Message.Content.Substring(2)];
                        await e.Message.RespondAsync($"{tag.Content}\n<{tag.Url}>");
                    }
                }
            };
        }
                
        public static Dictionary<string, Tag> Read()
        {
            Dictionary<string, Tag> output = new Dictionary<string, Tag>();
            string[] fileNames = Directory.GetFiles("Tags/");

            foreach (var fileName in fileNames)
            {
                output.Add(
                    new FileInfo(fileName).Name.Replace(".json", ""),
                    JsonConvert.DeserializeObject<Tag>(File.ReadAllText(fileName)));
            }

            return output;
        }
    }
}

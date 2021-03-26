using System.Collections.Generic;
using System.IO;
using DSharpPlus;
using Newtonsoft.Json;

namespace LazuriteBot.Modules.TagModule
{
    public class TagController
    {
        public static Dictionary<string, Tag> Tags { get; private set; }
        
        public static void Register(DiscordClient discord)
        {
            Tags = read();
            
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
                
        public static Dictionary<string, Tag> read()
        {
            Dictionary<string, Tag> output = new Dictionary<string, Tag>();
            string[] fileNames = Directory.GetFiles("Resources/Tags");

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
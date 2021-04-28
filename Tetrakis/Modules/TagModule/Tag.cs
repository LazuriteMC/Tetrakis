namespace LazuriteBot.Modules.TagModule
{
    public struct Tag
    {
        public string Content { get; }
        public string Url { get; }

        public Tag(string content, string url)
        {
            Content = content;
            Url = url;
        }
    }
}
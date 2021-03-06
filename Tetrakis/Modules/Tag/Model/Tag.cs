using System.Linq;

namespace Tetrakis.Modules.Tag.Model
{
    public record Tag(string Name, string Content, string Url, Tag[] Children)
    {
        public Tag GetChild(string name) => Children.First(child => child.Name == name);
    }
}
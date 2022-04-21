namespace SnippetApi.Models
{
    public class Command
    {
        public int CommandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Line { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}

namespace SnippetApi.Models
{
    public class Group
    {
        public Group()
        {
            Commands = new HashSet<Command>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Command> Commands { get; set; }
    }
}

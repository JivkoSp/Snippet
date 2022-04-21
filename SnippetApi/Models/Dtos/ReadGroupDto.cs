namespace SnippetApi.Models.Dtos
{
    public class ReadGroupDto
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

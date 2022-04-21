namespace SnippetApi.Models.Dtos
{
    public class ReadCommadDto
    {
        public int CommandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Line { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace SnippetApi.Models.Dtos
{
    public class UpdateCommandDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Line { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}

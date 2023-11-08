using System.ComponentModel.DataAnnotations;

namespace OpenAiAssistant
{
    public class OpenAiSettings
    {
        [Required]
        public string BaseUrl { get; init; } = "";
        [Required]
        public string ApiKey { get; init; } = "";
    }
}

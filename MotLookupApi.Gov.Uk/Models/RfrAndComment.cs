using System.Text.Json.Serialization;

namespace MotLookupApi.Gov.Uk.Models
{
    public class RfrAndComment
  {
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("dangerous")]
    public bool Dangerous { get; set; }
  }
}

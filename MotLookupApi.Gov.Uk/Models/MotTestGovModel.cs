using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MotLookupApi.Gov.Uk.Models
{
  public class MotTestGovModel
  {
    [JsonPropertyName("completedDate")]
    public DateTime CompletedDate { get; set; }
    [JsonPropertyName("testResult")]
    public string? TestResult { get; set; }
    [JsonPropertyName("expiryDate")]
    public DateTime ExpiryDate { get; set; }
    [JsonPropertyName("odometerValue")]
    public string? OdometerValue { get; set; }
    [JsonPropertyName("odometerUnit")]
    public string? OdometerUnit { get; set; }
    [JsonPropertyName("odometerResultType")]
    public string? OdometerResultType { get; set; }
    [JsonPropertyName("motTestNumber")]
    public long MotTestNumber { get; set; }
    [JsonPropertyName("rfrAndComments")]
    public ICollection<RfrAndComment>? RfrAndComments { get; set; }

  }
}

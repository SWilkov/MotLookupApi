using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MotLookupApi.Gov.Uk.Models
{
    public class VehicleGovModel
  {
    [JsonPropertyName("registration")]
    public string Registration { get; set; }
    [JsonPropertyName("make")]
    public string Make { get; set; }
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("firstUsedDate")]
    public DateTime FirstUsedDate { get; set; }
    
    /// <summary>
    /// MotTestDueDate is shown for new vehicles (less than 3 years old)
    /// </summary>
    [JsonPropertyName("motTestDueDate")]
    public DateTime MotTestDueDate { get; set; }
    [JsonPropertyName("fuelType")]
    public string? FuelType { get; set; }
    [JsonPropertyName("primaryColour")]
    public string? PrimaryColour { get; set; }
    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; set; }
    [JsonPropertyName("motTests")]
    public ICollection<MotTestGovModel> MotTests { get; set; }

    [JsonPropertyName("dvlaId")]
    public long DvlaId { get; set; }
    [JsonPropertyName("manufactureDate")]
    public DateTime? ManufactureDate { get; set; }
    [JsonPropertyName("registrationDate")]
    public DateTime? RegistrationDate { get; set; }
    [JsonPropertyName("engineSize")]
    public string EngineSize { get; set; }
  }
}

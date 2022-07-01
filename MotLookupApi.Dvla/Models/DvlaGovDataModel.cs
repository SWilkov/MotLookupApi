using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MotLookupApi.Dvla.Models
{
  public class DvlaGovDataModel
  {
    [JsonPropertyName("registrationNumber")]
    public string RegistrationNumber { get; set; }
    [JsonPropertyName("co2Emissions")]
    public int Co2Emissions { get; set; }
    [JsonPropertyName("engineCapicity")]
    public int EngineCapicity { get; set; }
    [JsonPropertyName("markedForExport")]
    public bool MarkedForExport { get; set; }
    [JsonPropertyName("fuelType")] 
    public string FuelType{ get; set; }
    [JsonPropertyName("motStatus")]
    public string MotStatus { get; set; }
    [JsonPropertyName("color")]
    public string Color { get; set; }
    [JsonPropertyName("make")]
    public string Make { get; set; }
    [JsonPropertyName("typeApproval")]
    public string TypeApproval { get; set; }
    [JsonPropertyName("yearOfManufacture")]
    public int YeatOfManufacture { get; set; }
    [JsonPropertyName("taxDueDate")]
    public DateTime TaxDueDate { get; set; }
    [JsonPropertyName("taxStatus")]
    public string TaxStatus { get; set; }
    [JsonPropertyName("dateOfLastV5CIssued")]
    public DateTime DateOfLastV5CIssued { get; set; }
    [JsonPropertyName("motExpiryDate")]
    public DateTime MotExpiryDate { get; set; }
    [JsonPropertyName("wheelplan")]
    public string Wheelplan { get; set; }
    [JsonPropertyName("monthOfFirstRegistration")]
    public string MonthOfFirstRegistration { get; set; }
  }
}

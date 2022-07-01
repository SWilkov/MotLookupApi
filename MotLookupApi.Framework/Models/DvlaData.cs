
namespace MotLookupApi.Framework.Models
{
  public class DvlaData
  {
    public string Registration { get; set; }
    public int Co2Emissions { get; set; }
    public int EngineCapicity { get; set; }
    public bool MarkedForExport { get; set; }
    public string FuelType { get; set; }
    public string MotStatus { get; set; }
    public string Color { get; set; }
    public string Make { get; set; }
    public string TypeApproval { get; set; }
    public DateTime TaxDueDate { get; set; }
    public string TaxStatus { get; set; }
    public DateTime DateOfLastV5CIssued { get; set; }
    public string Wheelplan { get; set; }
  }
}

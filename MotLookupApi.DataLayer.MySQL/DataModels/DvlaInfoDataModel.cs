using System.ComponentModel.DataAnnotations.Schema;

namespace MotLookupApi.DataLayer.MySQL.DataModels
{
  public class DvlaInfoDataModel : BaseDataModel
  {
    [Column("co2_emissions")]
    public int Co2Emissions { get; set; }
    [Column("marked_for_export")]
    public bool MarkedForExport { get; set; }
    [Column("type_approval")]
    public string TypeApproval { get; set; }
    [Column("tax_due_date")]
    public DateTime TaxDueDate { get; set; }
    [Column("tax_status")]
    public string TaxStatus { get; set; }
    [Column("date_last_v5c_issued")]
    public DateTime DateLastV5CIssued { get; set; }
    [Column("wheelplan")]
    public string Wheelplan { get; set; }
    [Column("vehicle_id")]
    public int VehicleId { get; set; }
  }
}

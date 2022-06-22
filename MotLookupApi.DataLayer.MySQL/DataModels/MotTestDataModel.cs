using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotLookupApi.DataLayer.MySQL.DataModels
{
  public class MotTestDataModel : BaseDataModel
  {    
    [Column("mileage")]
    [Required]
    public int Mileage { get; set; }
    [Column("completed_date")]
    [Required]
    public DateTime CompletedDate { get; set; }
    [Column("expiry_date")]
    [Required]
    public DateTime ExpiryDate { get; set; }
    [Column("test_number")]
    public long TestNumber { get; set; }
    [Required]
    [Column("test_result")]
    public string TestResult { get; set; }
    [Column("odometer_unit")]
    public string OdometerUnit { get; set; }
    [Column("odometer_result_type")]
    public string OdometerResultType { get; set; }
    public ICollection<CommentDataModel> Comments { get; set; }
    [Column("vehicle_id")]
    [Required]
    public int VehicleId { get; set; }
    public VehicleDataModel Vehicle { get; set; }

  }
}



using System.ComponentModel.DataAnnotations.Schema;

namespace MotLookupApi.DataLayer.MySQL.DataModels
{
  public class VehicleQueryDataModel : BaseDataModel
  {
    [Column("vehicle_id")]
    public int VehicleId { get; set; }
  }
}

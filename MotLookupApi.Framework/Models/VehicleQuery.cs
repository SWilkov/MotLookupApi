using System;

namespace MotLookupApi.Framework.Models
{
  public class VehicleQuery : Base
  {
    public DateTime CreatedAt { get; set; }
    public int VehicleId { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MotLookupApi.Framework.Models
{
  public class Vehicle : Base
  {
    public string Registration { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string PrimaryColour { get; set; }
    public string FuelType { get; set; }
    public DateTime FirstUsedDate { get; set; }
    public ICollection<MotTest> MotTests { get; set; }
    public ICollection<MileageStatistics> MileageStatistics { get; set; }
    public ICollection<VehicleQuery> VehicleQueries { get; set; }

    public DateTime MotTestDueDate { get; set; }
    public long? DvlaId { get; set; }
    public DateTime ManufactureDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string EngineSize { get; set; }
    public string UniqueVehicleId { get; set; }

    public DvlaData DvlaData { get; set; }
  }
}

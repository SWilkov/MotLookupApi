
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MotLookupApi.DataLayer.MySQL.DataModels
{
  public class VehicleDataModel : BaseDataModel
  {    
    [Column("make")]
    [Required]
    public string Make { get; set; }
    [Column("model")]
    [Required]
    public string Model { get; set; }
    [Column("unique_vehicle_id")]
    [Required]
    public string UniqueVehicleId { get; set; }
    [Column("primary_colour")]
    public string? PrimaryColour { get; set; }
    [Column("registration")]
    [Required]
    public string Registration { get; set; }
    [Column("first_used_date")]
    public DateTime FirstUsedDate { get; set; }
    [Column("fuel_type")]
    public string FuelType { get; set; }
    [Column("mot_test_due_date")]
    public DateTime MotTestDueDate { get; set; }
    [Column("dvla_id")]
    public long DvlaId { get; set; }
    [Column("manufacture_date")]
    public DateTime ManufactureDate { get; set; }
    [Column("registration_date")]
    public DateTime RegistrationDate { get; set; }
    [Column("engine_size")]
    public string EngineSize { get; set; }
    public ICollection<MotTestDataModel> MotTests { get; set; }
  }
}

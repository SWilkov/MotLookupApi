using MotLookupApi.Framework.Models;
using MotLookupApi.Gov.Uk.Interfaces;
using MotLookupApi.Gov.Uk.Models;

namespace MotLookupApi.Gov.Uk.Mappers
{
  public class VehicleGovMapper : IVehicleGovMapper
  {
    public VehicleGovMapper()
    { }

    public Vehicle Map(VehicleGovModel vehicleGovModel)
    {
      if (vehicleGovModel == null)
        return null;
      
      var vehicle = new Vehicle
      {        
        Make = vehicleGovModel.Make,  
        Model = vehicleGovModel.Model,
        PrimaryColour = vehicleGovModel.PrimaryColour ?? String.Empty,
        Registration = vehicleGovModel.Registration,
        FuelType = vehicleGovModel.FuelType ?? "N/A",
        EngineSize = vehicleGovModel.EngineSize ?? String.Empty,
        ManufactureDate = vehicleGovModel.ManufactureDate.HasValue ? vehicleGovModel.ManufactureDate.Value : DateTime.MinValue,
        RegistrationDate = vehicleGovModel.RegistrationDate.HasValue ? vehicleGovModel.RegistrationDate.Value : DateTime.MinValue,
        FirstUsedDate = vehicleGovModel.FirstUsedDate,
        MotTestDueDate = vehicleGovModel.MotTestDueDate,
        DvlaId = vehicleGovModel.DvlaId,
        UniqueVehicleId = vehicleGovModel.VehicleId ?? String.Empty
      };

      if (vehicleGovModel.MotTests != null)
      {
        vehicle.MotTests = vehicleGovModel.MotTests.Select(x =>
            new MotTest
            {
              CompletedDate = x.CompletedDate, 
              ExpiryDate = x.ExpiryDate,
              Mileage = Convert.ToInt32(x.OdometerValue),
              Result = x.TestResult, 
              OdometerResultType = x.OdometerResultType ?? String.Empty, 
              OdometerUnit = x.OdometerUnit ?? String.Empty,
              MotTestNumber = Convert.ToInt64(x.MotTestNumber),
              Comments = x.RfrAndComments == null ? new List<Comment>()
                : x.RfrAndComments.Select(c =>
                new Comment
                {
                  Text = c.Text,
                  Type = c.Type,
                  Dangerous = c.Dangerous
                }).ToList()
            }).ToList();
      }

      return vehicle;
    }
  }
}

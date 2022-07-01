using MotLookupApi.Dvla.Interfaces;
using MotLookupApi.Dvla.Models;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Dvla.Mappers
{
  public class DvlaDataMapper : IDvlaDataMapper
  {
    public DvlaData Map(DvlaGovDataModel source)
    {
      if (source == null) throw new ArgumentNullException("source");

      return new DvlaData
      {
        Make = source.Make,
        TaxDueDate = source.TaxDueDate,
        TaxStatus = source.TaxStatus,
        Co2Emissions = source.Co2Emissions,
        Color = source.Color,
        DateOfLastV5CIssued = source.DateOfLastV5CIssued,
        EngineCapicity = source.EngineCapicity,
        FuelType = source.FuelType,
        MarkedForExport = source.MarkedForExport,
        MotStatus = source.MotStatus,
        Registration = source.RegistrationNumber,
        TypeApproval = source.TypeApproval,
        Wheelplan = source.Wheelplan,
      };
    }
  }
}

using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.DataLayer.MySQL.Interfaces;
using MotLookupApi.Framework.Models;
using System;

namespace MotLookupApi.DataLayer.MySQL.Mappers
{
  public class MotTestMapper : IMotTestMapper
  {
    private readonly ICommentMapper _mapper;

    public MotTestMapper(ICommentMapper mapper)
    {
      _mapper = mapper;
    }
    public MotTestDataModel Map(MotTest test, int vehicleId)
    {
      if (test == null)
        throw new ArgumentNullException(nameof(test));
      if (vehicleId == default(int))
        throw new ArgumentException("Invalid vehicleId");

      var dm = Map(test);
      dm.VehicleId = vehicleId;

      return dm;
    }

    public MotTestDataModel Map(MotTest source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      var dm = new MotTestDataModel
      {
        Id = source.Id,
        CompletedDate = source.CompletedDate,
        ExpiryDate = source.ExpiryDate,
        TestNumber = source.MotTestNumber,
        Mileage = source.Mileage,
        TestResult = source.Result ?? "", 
        OdometerResultType = source.OdometerResultType ?? "",
        OdometerUnit = source.OdometerUnit ?? "",
        Comments = source.Comments == null || !source.Comments.Any() ? null : 
          source.Comments.Select(c => _mapper.Map(c)).ToList()
      };
      return dm;
    }

    public MotTest Map(MotTestDataModel source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new MotTest
      {
        Id = source.Id,
        CompletedDate = source.CompletedDate,
        ExpiryDate = source.ExpiryDate,
        Mileage = source.Mileage,
        Result = source.TestResult,
        MotTestNumber = source.TestNumber,
        OdometerResultType = source.OdometerResultType,
        OdometerUnit = source.OdometerUnit,
        Comments = source.Comments == null || !source.Comments.Any() ? null :
          source.Comments.Select(c => _mapper.Map(c)).ToList()
      };
    }
  }
}

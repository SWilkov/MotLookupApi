using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Interfaces
{
  public interface IMotTestMapper : IMapper<MotTestDataModel, MotTest>, IMapper<MotTest, MotTestDataModel>
  {
    MotTestDataModel Map(MotTest test, int vehicleId);
  }
}

using MediatR;
using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Queries
{
  public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesEvent, IEnumerable<Vehicle>>
  {
    private readonly IVehicleReadRepository _vehicleReadRepository;

    public GetAllVehiclesHandler(IVehicleReadRepository vehicleReadRepository)
    {
      _vehicleReadRepository = vehicleReadRepository;
    }
    public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesEvent request, CancellationToken cancellationToken)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));

      return await _vehicleReadRepository.GetAll();
    }
  }

  public class GetAllVehiclesEvent : IRequest<IEnumerable<Vehicle>>
  {

  }
}

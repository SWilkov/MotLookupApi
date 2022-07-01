using MediatR;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Queries
{
  public class GetOldCarsToUpdateHandler : IRequestHandler<GetOldCarsToUpdateEvent, IEnumerable<Vehicle>>
  {
    public async Task<IEnumerable<Vehicle>> Handle(GetOldCarsToUpdateEvent request, CancellationToken cancellationToken)
  {
    if (request == null) throw new ArgumentNullException(nameof(request));
    if (request.VehiclesToCheck == null) throw new ArgumentNullException(nameof(request.VehiclesToCheck));

      var oldCars = request.VehiclesToCheck
                           .Where(x => (x.MotTests != null && x.MotTests.Any()) &&
                                        x.MotTests.FirstOrDefault().ExpiryDate >= DateTime.Now &&
                                        DateTime.Now >= x.MotTests.FirstOrDefault().ExpiryDate.AddDays(-request.DaysBeforeMotDue));
    return oldCars;
  }
}

public class GetOldCarsToUpdateEvent : IRequest<IEnumerable<Vehicle>>
{
  public IEnumerable<Vehicle> VehiclesToCheck { get; private set; }
  public int DaysBeforeMotDue { get; private set; }
  public GetOldCarsToUpdateEvent(IEnumerable<Vehicle> vehiclesToCheck,
    int daysBeforeMotDue)
  {
    DaysBeforeMotDue = daysBeforeMotDue;
    VehiclesToCheck = vehiclesToCheck;
  }
}
}

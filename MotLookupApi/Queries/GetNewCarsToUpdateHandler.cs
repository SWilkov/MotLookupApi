using MediatR;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Queries
{
  public class GetNewCarsToUpdateHandler : IRequestHandler<GetNewCarsToUpdateEvent, IEnumerable<Vehicle>>
  {
    public async Task<IEnumerable<Vehicle>> Handle(GetNewCarsToUpdateEvent request, CancellationToken cancellationToken)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));
      if (request.VehiclesToCheck == null) throw new ArgumentNullException(nameof(request.VehiclesToCheck));

      var newCars = request.VehiclesToCheck
                           .Where(x => (x.MotTests == null || !x.MotTests.Any()) &&
                                       x.MotTestDueDate != DateTime.MinValue &&
                                       x.MotTestDueDate >= DateTime.Now &&
                                       DateTime.Now >= x.MotTestDueDate.AddDays(-request.DaysBeforeMotDue));
      return newCars;
    }
  }

  public class GetNewCarsToUpdateEvent : IRequest<IEnumerable<Vehicle>>
  {
    public IEnumerable<Vehicle> VehiclesToCheck { get; private set; }
    public int DaysBeforeMotDue { get;private set; }
    public GetNewCarsToUpdateEvent(IEnumerable<Vehicle> vehiclesToCheck,
      int daysBeforeMotDue)
    {
      DaysBeforeMotDue = daysBeforeMotDue;
      VehiclesToCheck = vehiclesToCheck;
    }
  }
}

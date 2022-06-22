using MediatR;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Events
{
  public class VehicleRetrievedEvent : INotification
  {
    public Vehicle Vehicle { get; set; }
    public double AverageMilesPerYear { get; set; }
    public double AverageMilesFirstMot { get; set; }
    public int YearsToFirstMot { get; set; }
  }
}

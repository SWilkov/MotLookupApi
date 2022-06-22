using MediatR;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Events
{
  public class NewVehicleRetrievedEvent : INotification
  {
    public Vehicle Vehicle { get; set; }    
  }
}

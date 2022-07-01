using MediatR;
using MotLookupApi.Framework.Models;
using MotLookupApi.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MotLookupApiTests
{
  public class GetOldCarsToUpdateHandlerTests
  {
    private readonly IRequestHandler<GetOldCarsToUpdateEvent, IEnumerable<Vehicle>> _handler;
    public GetOldCarsToUpdateHandlerTests()
    {
      _handler = new GetOldCarsToUpdateHandler();
    }

    [Fact]
    public async Task request_is_null_throw_excption()
    {
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(null, cts.Token));
    }

    [Fact]
    public async Task vehicle_is_null_throw_excption()
    {
      var request = new GetOldCarsToUpdateEvent(null, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(request, cts.Token));
    }

    [Fact]
    public async Task new_car_return_none()
    {
      var cars = new List<Vehicle>
      {
        new Vehicle
        {
          Id = 1,
          Make = "Volvo",
          Registration = "AE15XTD",
          MotTestDueDate = DateTime.Now.AddMonths(3),
          MotTests = null
        }
      };
      var request = new GetOldCarsToUpdateEvent(cars, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

      var newCars = await _handler.Handle(request, cts.Token);

      Assert.NotNull(newCars);
      Assert.Empty(newCars);
    }

    [Fact]
    public async Task old_car_with_long_expiry_return_none()
    {
      var cars = new List<Vehicle>
      {
        new Vehicle
        {
          Id = 1,
          Make = "Volvo",
          Registration = "AE15XTD",
          MotTestDueDate = DateTime.MinValue,
          MotTests = new List<MotTest>
          {
            new MotTest
            {
              ExpiryDate = DateTime.Now.AddMonths(6),
            }
          }
        }
      };
      var request = new GetOldCarsToUpdateEvent(cars, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

      var newCars = await _handler.Handle(request, cts.Token);

      Assert.NotNull(newCars);
      Assert.Empty(newCars);
    }

    [Fact]
    public async Task old_car_with_short_expiry_return_one()
    {
      var cars = new List<Vehicle>
      {
        new Vehicle
        {
          Id = 1,
          Make = "Volvo",
          Registration = "AE15XTD",
          MotTestDueDate = DateTime.MinValue,
          MotTests = new List<MotTest>
          {
            new MotTest
            {
              ExpiryDate = DateTime.Now.AddMonths(1),
            }
          }
        }
      };
      var request = new GetOldCarsToUpdateEvent(cars, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

      var newCars = await _handler.Handle(request, cts.Token);

      Assert.NotNull(newCars);
      Assert.Single(newCars);
    }
  }
}

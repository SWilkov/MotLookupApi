using MediatR;
using MotLookupApi.Framework.Models;
using MotLookupApi.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace MotLookupApiTests
{
  public class GetNewCarsToUpdateHandlerTests
  {
    private readonly IRequestHandler<GetNewCarsToUpdateEvent, IEnumerable<Vehicle>> _handler;
    public GetNewCarsToUpdateHandlerTests()
    {
      _handler = new GetNewCarsToUpdateHandler();
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
      var request = new GetNewCarsToUpdateEvent(null, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(request, cts.Token));
    }

    [Fact]
    public async Task old_car_return_none()
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
              Id = 1,
              ExpiryDate = DateTime.MinValue
            }
          }
        }
      };
      var request = new GetNewCarsToUpdateEvent(cars, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

      var newCars = await _handler.Handle(request, cts.Token);

      Assert.NotNull(newCars);
      Assert.Empty(newCars);
    }

    [Fact]
    public async Task new_car_mot_test_more_than_60_days_ahead_return_none()
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
      var request = new GetNewCarsToUpdateEvent(cars, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

      var newCars = await _handler.Handle(request, cts.Token);

      Assert.NotNull(newCars);
      Assert.Empty(newCars);
    }

    [Fact]
    public async Task new_car_mot_test_less_than_60_days_ahead_return_one()
    {
      var cars = new List<Vehicle>
      {
        new Vehicle
        {
          Id = 1,
          Make = "Volvo",
          Registration = "AE15XTD",
          MotTestDueDate = DateTime.Now.AddDays(50),
          MotTests = null
        }
      };
      var request = new GetNewCarsToUpdateEvent(cars, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

      var newCars = await _handler.Handle(request, cts.Token);

      Assert.NotNull(newCars);
      Assert.Single(newCars);
    }

    [Fact]
    public async Task new_car_mot_test_greater_than_60_days_by_one_day_return_none()
    {
      var cars = new List<Vehicle>
      {
        new Vehicle
        {
          Id = 1,
          Make = "Volvo",
          Registration = "AE15XTD",
          MotTestDueDate = DateTime.Now.AddDays(61),
          MotTests = null
        }
      };
      var request = new GetNewCarsToUpdateEvent(cars, 60);
      var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

      var newCars = await _handler.Handle(request, cts.Token);

      Assert.NotNull(newCars);
      Assert.Empty(newCars);
    }   
  }
}
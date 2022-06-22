using System;
using System.Collections.Generic;
using System.Text;

namespace MotLookupApi.Framework.Models
{
  public sealed class FuelType
  {
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private static readonly Dictionary<string, FuelType> Instances = new Dictionary<string, FuelType>();
    private static readonly Dictionary<int, FuelType> IdInstances = new Dictionary<int, FuelType>();

    public static readonly FuelType Petrol = new FuelType(1, "Petrol", "Unleaded");
    public static readonly FuelType Diesel = new FuelType(2, "Diesel", "Popular in Europe");
    public static readonly FuelType HybridElectric = new FuelType(3, "Hybrid Electric (clean)", "Petrol plus Electric engines");
    public static readonly FuelType Electric = new FuelType(4, "Electric", "Fully powered by electric");
    public static readonly FuelType Unknown = new FuelType(5, "Unknown", "unknown fuel type");

    private FuelType(int id, string name, string description)
    {
      this.Id = id;
      this.Name = name;
      this.Description = description;

      Instances[name.ToLower()] = this;
      IdInstances[id] = this;
    }

    //Casting
    public static explicit operator FuelType(int id)
    {
      if (id == default(int))
        id = 1;

      FuelType result;
      if (IdInstances.TryGetValue(id, out result))
        return result;
      throw new InvalidCastException($"Error casting {id}");
    }

    public static explicit operator FuelType(string name)
    {
      if (string.IsNullOrEmpty(name))
        name = "Petrol";

      FuelType result;
      if (Instances.TryGetValue(name.ToLower(), out result))
        return result;
      throw new InvalidCastException($"Error casting {name}");
    }

    public override string ToString()
    {
      return Name.ToLower();
    }
  }
}

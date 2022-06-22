using System;
using System.Collections.Generic;
using System.Text;

namespace MotLookupApi.DataLayer.Interfaces
{
  public interface IMapper<TDestination, TSource>
    where TDestination: class
    where TSource: class
  {
    TDestination Map(TSource source);
  }
}

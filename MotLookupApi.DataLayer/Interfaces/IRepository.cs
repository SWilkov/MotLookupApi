using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MotLookupApi.DataLayer.Interfaces
{
  public interface IRepository<T>
    where T: class
  {
    Task<T> Save(T item);
    Task Delete(T item);
  }
}

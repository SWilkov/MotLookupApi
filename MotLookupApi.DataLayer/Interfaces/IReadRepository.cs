using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MotLookupApi.DataLayer.Interfaces
{
  public interface IReadRepository<T>
    where T: class
  {
    T Get(int id);
    Task<IEnumerable<T>> GetByParent(int parentId);
  }
}

using System.Threading.Tasks;

namespace MotLookupApi.Framework.Interfaces
{
  public interface ISaveService<T>
    where T: class
  {
    Task<T> Save(T item);
  }
}

using System.Linq;

namespace D2DTechSampleApplication.Code
{
    public interface IRepository<T>
    {
        void Create(T obj);
        IQueryable<T> Retrieve();
        void Update(T obj);
    }
}
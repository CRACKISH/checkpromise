using System.Linq;

namespace Checkpromise.Persistence
{
    public interface IRepository<T>
    {
        void Add(T entity);

        IQueryable<T> GetAll();
    }
}

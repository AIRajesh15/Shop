using Shop.Core.Models;
using System.Linq;

namespace Shop.DataAccess.InMemory
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        T Find(string Id);
        void Insert(T t);
        void Delete(string Id);
        void Update(T t);
    }
}
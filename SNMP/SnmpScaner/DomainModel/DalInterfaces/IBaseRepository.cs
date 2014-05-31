using System.Collections.Generic;

namespace DomainModel.DalInterfaces
{
    public interface IBaseRepository<T>
    {
        void Add(T item);
        T GetById(object id);
        IEnumerable<T> GetAll();
        void RemoveById(object id);
        void SaveChanges();
    }
}

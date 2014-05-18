using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
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

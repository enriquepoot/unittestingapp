using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<T, TDb>
    {
        IEnumerable<T> GetAll();
        T GetByID(Guid id);
        void Add(T obj);
        void Update(T obj);
        void Delete(Guid id);
        IUnitOfWork UnitOfWork { set; }
    }
}

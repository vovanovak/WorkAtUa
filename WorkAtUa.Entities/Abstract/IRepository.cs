using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public interface IRepository<T> where T: Entity
    {
        List<T> GetAll();
        bool Save(T entity);
        bool Delete(int id);
        bool Delete(T entity);
        Entity GetEntityById(int id);
    }
}

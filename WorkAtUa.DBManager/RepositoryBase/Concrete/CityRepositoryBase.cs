using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public class CityRepositoryBase : RepositoryBase<MyCity, City>
    {
        protected override System.Data.Entity.DbSet<City> GetTable()
        {
            return ctx.Cities;
        }

        protected override System.Linq.Expressions.Expression<Func<City, MyCity>> GetConverter()
        {
            return u => new MyCity()
            {
                Id = u.Id,
                Name = u.Name
            };
        }


        protected override void UpdateEntry(City dbEntity, MyCity entity)
        {
            dbEntity.Name = entity.Name;
        }

       
    }
}
